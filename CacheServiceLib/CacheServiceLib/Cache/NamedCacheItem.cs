using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheServiceLib.Cache
{
    /*
     * NamedCacheItem is the item for NamedCache class
     * it contains all the information needed for named cache
     */
    public class NamedCacheItem<TCacheKey>
    {
        public string Name;
        public ICache<TCacheKey> Cache;
        public CacheKeyLock<TCacheKey> Locks;
    }
}
