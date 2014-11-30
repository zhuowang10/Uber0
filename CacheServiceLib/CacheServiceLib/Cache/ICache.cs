using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheServiceLib.Cache
{
    /*
     * this is the interface for time expiration cache
     */

    public interface ICache<TKey>
    {
        //get cache value from a cache key, return null if no value cached
        object Get(TKey key);

        //set cache value from a cache key with time expiration
        void Set(TKey key, object value, int idleDuration);

        //remove cache value
        void Remove(TKey key);
    }
}
