using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheServiceLib.HelloWorld
{
    class HelloWorldRepository : MarshalByRefObject, IHelloWorldRepository
    {
        public string DALGetText(string name)
        {
            //pretend we're doing something heavy for 5 seconds
            System.Threading.Thread.Sleep(5000);
            return "hello " + name;
        }
    }
}
