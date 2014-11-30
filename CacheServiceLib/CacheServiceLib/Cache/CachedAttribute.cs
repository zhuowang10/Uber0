using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace CacheServiceLib.Cache
{
    /*
     * cache attribute class provides easy access to setup cache attributes in []
     * like [Cached(60)]
     * 
     */

    [AttributeUsage(AttributeTargets.Method)]
    public class CachedAttribute : Attribute
    {
        private const int _defaultDuration = 60;
        private const string _defaultCacheName = null;

        public int Duration { get; set; }
        public string CacheName { get; set; }

        public CachedAttribute()
        {
            Duration = _defaultDuration;
        }

        public CachedAttribute(int duration)
        {
            Duration = duration;
        }

        public CachedAttribute(int duration, string cacheName)
        {
            Duration = duration;
            CacheName = cacheName;
        }

        public static CachedAttribute Get(MethodBase method)
        {
            var cachedAttribute =
                method.GetCustomAttributes(typeof(CachedAttribute), true).FirstOrDefault() as CachedAttribute;

            return cachedAttribute;
        }
    }
}
