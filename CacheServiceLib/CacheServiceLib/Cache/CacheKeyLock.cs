using System;
using System.Collections.Generic;
using System.Text;

namespace CacheServiceLib.Cache
{
    /*
     * a common use case is when trying to get a un-cached value from cache, init value first then store value to cache before returning result
     * the init process, however, usually is time-consuming, so there may be several calls in the init process before cache is available, 
     * therefore multiple init processes run.
     * this class is to maintain locks for each key in a cache object, and to make sure cache value is inited only once for concurrent calls
     * a exculsive lock is used to avoid thread confliction
     * this class is thread safe
     */

    public class CacheKeyLock<TKey>
    {
        private Dictionary<TKey, object> _locks = new Dictionary<TKey, object>();
        private object _lock = new object();

        public object GetLock(TKey key)
        {
            lock (_lock)
            {
                if (!_locks.ContainsKey(key))
                {
                    object obj = new object();
                    _locks.Add(key, obj);
                    return obj;
                }
                else
                    return _locks[key];
            }
        }

        public void ReleaseLock(TKey key)
        {
            lock (_lock)
            {
                if (_locks.ContainsKey(key))
                    _locks.Remove(key);
            }
        }
    }
}
