using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CacheServiceLib.HelloWorld;

namespace CacheServiceLib
{
    /*
     * imeplements cache service function by calling helloworld class
     */
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class CacheService : ICacheService
    {
        [OperationBehavior]
        public string GetHelloWorldWithCache(string name)
        {
            return HelloWorld.HelloWorld.SayHelloWorld(name);
        }

        [OperationBehavior]
        public void ClearHelloWorldCache(string name)
        {
            HelloWorld.HelloWorld.RemoveCacheSayHelloWorld(name);
        }
    }
}
