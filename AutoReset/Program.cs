using System;
using System.Threading;

namespace Threading
{
    class AutoReset
    {
        static void Main(string[] args)
        {
            Counter cnt = new Counter();
            Thread[] cntThreads = new Thread[5];
            for (int i = 0; i < cntThreads.Length; i++)
            {
                Console.WriteLine(i);
                cntThreads[i] = new Thread(cnt.Count);
                cntThreads[i].Name = "Thread - " + i.ToString();
                cntThreads[i].Start();
            }
        }
        internal class Counter
        {
            private AutoResetEvent lockVar = new AutoResetEvent(true);
            public void Count()
            {
                while (true)
                {
                    lockVar.WaitOne();
                    Console.WriteLine(Thread.CurrentThread.Name);
                    Thread.Sleep(500);
                    lockVar.Set();
                }
            }
        }
    }
}