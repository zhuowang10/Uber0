using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheServiceLib.Cache
{
    /*
     * cache manager that manages multiple caches identified by different names
     * this class is thread safe
     */
    public class NamedCache<TCacheKey>
    {
        private Dictionary<string, NamedCacheItem<TCacheKey>> _namedCache = new Dictionary<string, NamedCacheItem<TCacheKey>>();
        private object _nameCacheLock = new object();

        public NamedCacheItem<TCacheKey> GetNamedCache(string name, Type newCacheType)
        {
            lock (_nameCacheLock)
            {
                if (!_namedCache.ContainsKey(name))
                {
                    NamedCacheItem<TCacheKey> item = new NamedCacheItem<TCacheKey>()
                    {
                        Cache = (ICache<TCacheKey>)Activator.CreateInstance(newCacheType, true),
                        Locks = new CacheKeyLock<TCacheKey>(),
                        Name = name
                    };
                    _namedCache.Add(name, item);
                    return item;
                }
                else
                    return _namedCache[name];
            }
        }

    }
}
