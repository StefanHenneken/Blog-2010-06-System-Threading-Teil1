using System;
using System.Threading;

namespace Threading
{
    class ManualResetEvent2
    {
        static void Main(string[] args)
        {
            Counter cnt = new Counter();
            Thread[] cntThreads = new Thread[15];
            cnt.locks = new AutoResetEvent[cntThreads.Length];
            for (int i = 0; i < cntThreads.Length; i++)
            {
                cnt.locks[i] = new AutoResetEvent(false);
                cntThreads[i] = new Thread(cnt.Count);
                cntThreads[i].Name = " Thread - " + i.ToString();
                cntThreads[i].Start();
            }
            Console.ReadLine();
            cnt.startLock.Set();
        }
    }
    internal class Counter
    {
        public AutoResetEvent[] locks;
        public ManualResetEvent startLock = new ManualResetEvent(false);
        private int value = 0;
        private int idCount = 0;
        public void Count()
        {
            int myId;
            lock (this)
            {
                myId = idCount++;
                if (myId == 0) locks[0].Set();
            }
            startLock.WaitOne();
            while (true)
            {
                locks[myId].WaitOne();
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(" {0}: {1}", Thread.CurrentThread.Name, value++);
                    Thread.Sleep(500);
                }
                if ((myId + 1) >= locks.Length)
                    locks[0].Set();
                else
                    locks[myId + 1].Set();
            }
        }
    }
}
