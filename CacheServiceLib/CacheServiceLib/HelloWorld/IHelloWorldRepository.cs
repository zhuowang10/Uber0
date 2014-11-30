using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheServiceLib.Cache;

namespace CacheServiceLib.HelloWorld
{
    public interface IHelloWorldRepository
    {
        [Cached(1)]
        string DALGetText([CacheKey] string name);

    }
}
