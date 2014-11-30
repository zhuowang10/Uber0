using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace CacheServiceLib.Cache
{
    /*
     * CacheProxy modifies function runtime call flow, allowing cache implementation within the flow
     * it also makes sure only one cache value initialization allowed for each cache key in concurrency (CacheInvoke function)
     * MarshalByRefObject is to enable cache proxy can do remote call
     */

    public class CacheProxy : RealProxy
    {
        private readonly MarshalByRefObject _targetObj;
        private static NamedCache<string> _namedCache = new NamedCache<string>();

        private CacheProxy(Type type, MarshalByRefObject targetObj)
            : base(type)
        {
            _targetObj = targetObj;
        }

        public static T CreateCacheProxy<T>() where T : MarshalByRefObject
        {
            return
                (T)
                (new CacheProxy(typeof(T), (MarshalByRefObject)Activator.CreateInstance(typeof(T), true))).
                    GetTransparentProxy();
        }

        public static T CreateCacheProxy<T>(params object[] args) where T : MarshalByRefObject
        {
            return
                (T)
                (new CacheProxy(typeof(T), (MarshalByRefObject)Activator.CreateInstance(typeof(T), args, true))).
                    GetTransparentProxy();
        }

        public override IMessage Invoke(IMessage msg)
        {
            IMessage returnMsg = null;
            var msgCall = msg as IMethodCallMessage;
            if (msgCall == null)
            {
                throw new System.Exception("msg can not be null");
            }

            CachedAttribute cachedAttribute = CachedAttribute.Get(msgCall.MethodBase);

            if (cachedAttribute != null)
            {
                returnMsg = CacheInvoke(msgCall, cachedAttribute);
            }
            else
            {
                returnMsg = RemotingServices.ExecuteMessage(_targetObj, msgCall);
            }

            return returnMsg;
        }

        //remove a cache item of one function without args
        static public void RemoveFunctionCache<T>(string methodName)
        {
            RemoveFunctionCache<T>(methodName, null, null);
        }

        //remove a cache item of one function with specific args
        static public void RemoveFunctionCache<T>(string methodName, object[] args)
        {
            var n = args.Length;
            var types = new Type[n];
            for (int i = 0; i < n; i++)
            {
                if (args != null)
                    types[i] = args[i].GetType();
            }

            RemoveFunctionCache<T>(methodName, types, args);
        }

        static public void RemoveFunctionCache<T>(string methodName, Type[] types, object[] args)
        {
            MethodBase methodBase;
            if (types == null)
                methodBase = typeof(T).GetMethod(methodName);
            else
                methodBase = typeof(T).GetMethod(methodName, types);

            if (methodBase == null)
                return;

            CachedAttribute cachedAttribute = CachedAttribute.Get(methodBase);
            if (cachedAttribute != null)
            {
                string cacheName = cachedAttribute.CacheName ?? methodBase.ReflectedType.FullName;
                string key = GetCacheKey(methodBase, args);
                NamedCacheItem<string> item = _namedCache.GetNamedCache(cacheName, typeof(MemCache<string>));
                object cacheKeyLock = item.Locks.GetLock(key);
                try
                {
                    lock (cacheKeyLock)
                    {
                        item.Cache.Remove(key);
                    }
                }
                finally
                {
                    item.Locks.ReleaseLock(key);
                }
            }
        }

        private IMessage CacheInvoke(IMethodCallMessage msg, CachedAttribute cachedAttribute)
        {
            string cacheName = cachedAttribute.CacheName ?? msg.MethodBase.ReflectedType.FullName;
            string key = GetCacheKey(msg);

            NamedCacheItem<string> item = _namedCache.GetNamedCache(cacheName, typeof(MemCache<string>));

            object cacheKeyLock = item.Locks.GetLock(key);
            try
            {
                lock (cacheKeyLock)
                {
                    object returnValue = item.Cache.Get(key);
                    if (returnValue == null)    //no cache
                    {
                        IMethodReturnMessage returnMessage = RemotingServices.ExecuteMessage(_targetObj, msg);
                        if (returnMessage.Exception != null)
                            return returnMessage;
                        item.Cache.Set(key, returnMessage.ReturnValue ?? new ReturnValueNull(), cachedAttribute.Duration);
                        return new ReturnMessage(returnMessage.ReturnValue, null, 0, msg.LogicalCallContext, msg);
                    }
                    else
                        return new ReturnMessage(returnValue is ReturnValueNull ? null : returnValue, null, 0, msg.LogicalCallContext, msg);
                }
            }
            finally
            {
                item.Locks.ReleaseLock(key);
            }
        }

        private static string GetCacheKey(IMethodCallMessage msg)
        {
            return GetCacheKey(msg.MethodBase, msg.Args);
        }

        private static string GetCacheKey(MethodBase methodBase, object[] args)
        {
            CacheKey cacheKey = new FunctionCacheKey(methodBase, args);
            return cacheKey.KeyValue;
        }
    }

    class ReturnValueNull
    {
    }

}
