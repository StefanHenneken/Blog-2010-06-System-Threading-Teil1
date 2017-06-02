using System;
using System.Threading;

namespace Threading
{
    public class MutexTest
    {
        public static Mutex MutexLock;
        public static void Main(string[] args)
        {
            MutexLock = new Mutex();
            for (int i = 0; i < 10; i++)
            {
                Thread t = new Thread(
                    delegate()
                    {
                        while (true)
                        {
                            MutexTest.MutexLock.WaitOne();
                            Console.WriteLine("aktiver Thread: {0}", Thread.CurrentThread.Name);
                            Thread.Sleep(500);
                            MutexTest.MutexLock.ReleaseMutex();
                        }
                    } );
                t.Name = "Thread - " + i.ToString();
                t.Start();
            }
        }
    }
}
