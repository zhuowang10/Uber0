using System;
using System.Linq;
using System.Reflection;

namespace CacheServiceLib.Cache
{
    /*
     * CacheKeyAttribute class provides easy access to setup cache key attributes in [] for a function
     * like IEnumerable<int> DALGetUserList([CacheKey] Int32 userGroupID);
     */
    [AttributeUsage(AttributeTargets.Parameter)]
    public class CacheKeyAttribute : Attribute
    {
        public static CacheKeyAttribute Get(ParameterInfo param)
        {
            var cachedKeyAttribute =
                param.GetCustomAttributes(typeof(CacheKeyAttribute), true).FirstOrDefault() as CacheKeyAttribute;

            return cachedKeyAttribute;
        }
    }
}
