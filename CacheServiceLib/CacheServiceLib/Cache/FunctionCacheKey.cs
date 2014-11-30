using System.Collections;
using System.Reflection;

namespace CacheServiceLib.Cache
{
    /*
     * derived from CacheKey class, FunctionCacheKey setup function name and function parameters to get unique key
     */
    public class FunctionCacheKey : CacheKey
    {
        private readonly object[] _args;
        private readonly MethodBase _method;

        public FunctionCacheKey(MethodBase method, params object[] args)
        {
            _method = method;
            _args = args;
        }

        protected override IList GetKeyItemList()
        {
            IList items = new ArrayList();
            items.Add(string.Format("{0}_", _method.MetadataToken));

            ParameterInfo[] parameters = _method.GetParameters();

            foreach (ParameterInfo pInfo in parameters)
            {
                CacheKeyAttribute cachedKeyAttribute = CacheKeyAttribute.Get(pInfo);
                if (!pInfo.IsOut && cachedKeyAttribute != null)
                {
                    items.Add(_args[pInfo.Position]);
                    items.Add(",");
                }
            }

            return items;
        }
    }
}
