using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using CacheServiceLib.HelloWorld;
using log4net;

namespace TestCache
{
    /*
     * test case 1:
     * test continuious function calls and record function call latency
     * expections: first call should be time consuming, others following should be instant, except the last one, where cache is expired
     * 
     * test case 2:
     * test different parameters
     * expections: different caches match different parameters
     * 
     * test case 3:
     * test clear cache by non cachekey parameter and cachekey parameter
     * non-cachekey parameter clear cache should not work (cache still works), cachekey parameter clear cache works
     */
    public class TestCase
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //test case 1: test continuious function calls and record function call latency
        public static void TestCase1()
        {
            _logger.Debug("test1 started");
            long startTick, tick;

            string name = "test1";

            _logger.Debug("Function call 1");
            startTick = DateTime.UtcNow.Ticks;
            _logger.Debug(HelloWorld.SayHelloWorld(name));
            tick = DateTime.UtcNow.Ticks;
            _logger.DebugFormat("latency: {0}", tick - startTick);

            _logger.Debug("Function call 2");
            startTick = DateTime.UtcNow.Ticks;
            _logger.Debug(HelloWorld.SayHelloWorld(name));
            tick = DateTime.UtcNow.Ticks;
            _logger.DebugFormat("latency: {0}", tick - startTick);

            //wait 65 seconds (cache idle duration is 1 minute)
            System.Threading.Thread.Sleep(65 * 100000);

            _logger.Debug("Function call 3");
            startTick = DateTime.UtcNow.Ticks;
            _logger.Debug(HelloWorld.SayHelloWorld(name));
            tick = DateTime.UtcNow.Ticks;
            _logger.DebugFormat("latency: {0}", tick - startTick);
        }

        //test case 2: test different parameters
        public static void TestCase2()
        {
            _logger.Debug("test2 started");
            long startTick, tick;

            string name1 = "p1";
            string name2 = "p2";

            _logger.DebugFormat("Function call with parameter {0}", name1);
            startTick = DateTime.UtcNow.Ticks;
            _logger.Debug(HelloWorld.SayHelloWorld(name1));
            tick = DateTime.UtcNow.Ticks;
            _logger.DebugFormat("latency: {0}", tick - startTick);

            _logger.DebugFormat("Function call with parameter {0}", name1);
            startTick = DateTime.UtcNow.Ticks;
            _logger.Debug(HelloWorld.SayHelloWorld(name1));
            tick = DateTime.UtcNow.Ticks;
            _logger.DebugFormat("latency: {0}", tick - startTick);

            _logger.DebugFormat("Function call with parameter {0}", name2);
            startTick = DateTime.UtcNow.Ticks;
            _logger.Debug(HelloWorld.SayHelloWorld(name2));
            tick = DateTime.UtcNow.Ticks;
            _logger.DebugFormat("latency: {0}", tick - startTick);

            _logger.DebugFormat("Function call with parameter {0}", name1);
            startTick = DateTime.UtcNow.Ticks;
            _logger.Debug(HelloWorld.SayHelloWorld(name1));
            tick = DateTime.UtcNow.Ticks;
            _logger.DebugFormat("latency: {0}", tick - startTick);
        }

        //test case 3: test clear cache
        public static void TestCase3()
        {
            _logger.Debug("test3 started");
            long startTick, tick;

            string name = "test3";
            string dname = "foo";

            _logger.DebugFormat("Function call 1");
            startTick = DateTime.UtcNow.Ticks;
            _logger.Debug(HelloWorld.SayHelloWorld(name));
            tick = DateTime.UtcNow.Ticks;
            _logger.DebugFormat("latency: {0}", tick - startTick);

            _logger.DebugFormat("Function call 2");
            startTick = DateTime.UtcNow.Ticks;
            _logger.Debug(HelloWorld.SayHelloWorld(name));
            tick = DateTime.UtcNow.Ticks;
            _logger.DebugFormat("latency: {0}", tick - startTick);

            _logger.DebugFormat("Clear Cache with different parameter {0}", dname);
            startTick = DateTime.UtcNow.Ticks;
            _logger.Debug(HelloWorld.SayHelloWorld(dname));
            tick = DateTime.UtcNow.Ticks;
            _logger.DebugFormat("latency: {0}", tick - startTick);

            _logger.DebugFormat("Function call 3");
            startTick = DateTime.UtcNow.Ticks;
            _logger.Debug(HelloWorld.SayHelloWorld(name));
            tick = DateTime.UtcNow.Ticks;
            _logger.DebugFormat("latency: {0}", tick - startTick);

            _logger.DebugFormat("Clear Cache with different parameter {0}", name);
            startTick = DateTime.UtcNow.Ticks;
            _logger.Debug(HelloWorld.SayHelloWorld(name));
            tick = DateTime.UtcNow.Ticks;
            _logger.DebugFormat("latency: {0}", tick - startTick);

            _logger.DebugFormat("Function call 4");
            startTick = DateTime.UtcNow.Ticks;
            _logger.Debug(HelloWorld.SayHelloWorld(name));
            tick = DateTime.UtcNow.Ticks;
            _logger.DebugFormat("latency: {0}", tick - startTick);
        }

        //test case 4: multi-threading test
        public static void TestCase4(int readThreads, int clearThreads)
        {
            _logger.Debug("test4 started");
            string name = "test4";

            int n = readThreads + clearThreads;
            ManualResetEvent[] evts = new ManualResetEvent[n];
            for (int i = 0; i < n; i++)
                evts[i] = new ManualResetEvent(false);

            Random rand = new Random();
            int delay = 120 * 1000000;
            for (int i = 0; i < n; i++)
            {
                TestCacheCall t = new TestCacheCall(rand.Next(delay), name);
                if (i < readThreads)
                    ThreadPool.QueueUserWorkItem(t.TestCall, evts[i]);
                else
                    ThreadPool.QueueUserWorkItem(t.TestClearCall, evts[i]);
            }
        }
    }
}
