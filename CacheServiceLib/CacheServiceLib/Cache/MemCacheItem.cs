using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheServiceLib.Cache
{
    /*
     * MemCacheItem is the item for MemCache class
     * it contains all the information needed for cache and update to operate
     */
    public class MemCacheItem<Tkey>
    {
        public Tkey Key;
        public object Value;
        public int IdleDuration;
        public DateTime ExpirationTime;
    }
}
