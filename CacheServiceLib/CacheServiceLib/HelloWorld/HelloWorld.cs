using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheServiceLib.Cache;

namespace CacheServiceLib.HelloWorld
{
    public class HelloWorld : MarshalByRefObject
    {
        private static IHelloWorldRepository _cachedRepository;
        private static readonly object _cachedRepositoryLock = new object();

        protected static IHelloWorldRepository CachedRepository
        {
            get
            {
                lock (_cachedRepositoryLock)
                {
                    if (_cachedRepository == null)
                    {
                        _cachedRepository = CacheProxy.CreateCacheProxy<HelloWorldRepository>();
                    }
                    return _cachedRepository;
                }
            }
        }

        public static string SayHelloWorld(string name)
        {
            return CachedRepository.DALGetText(name);
        }

        public static void RemoveCacheSayHelloWorld(string name)
        {
            object[] oj = new object[1];
            oj[0] = name;
            CacheProxy.RemoveFunctionCache<IHelloWorldRepository>("DALGetText", oj);
        }
    }
}
