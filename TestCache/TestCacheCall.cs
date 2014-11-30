using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CacheServiceLib.HelloWorld;
using log4net;

namespace TestCache
{
    public class TestCacheCall
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private int _delay;
        private string _name;

        public TestCacheCall(int delay, string name)
        {
            this._delay = delay;
            this._name = name;
        }

        public void TestCall(object state)
        {
            Random rand = new Random();
            while (true)
            {
                long startTick = DateTime.UtcNow.Ticks;
                _logger.Debug(HelloWorld.SayHelloWorld(_name));
                long tick = DateTime.UtcNow.Ticks;
                _logger.DebugFormat("latency: {0}", tick - startTick);

                System.Threading.Thread.Sleep(rand.Next(_delay));
            }
        }

        public void TestClearCall(object state)
        {
            Random rand = new Random();
            while (true)
            {
                long startTick = DateTime.UtcNow.Ticks;
                HelloWorld.RemoveCacheSayHelloWorld(_name);
                long tick = DateTime.UtcNow.Ticks;
                _logger.DebugFormat("latency: {0}", tick - startTick);

                System.Threading.Thread.Sleep(rand.Next(_delay));
            }
        }
    }
}
