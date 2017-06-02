using System;
using System.Collections.Generic;
using System.Threading;

namespace Threading
{
    class HelloWorld2
    {
        private LinkedList<Thread> threads;
        private Thread exitThread = null;
        static void Main(string[] args)
        {
            new HelloWorld2();
        }
        public HelloWorld2()
        {
            threads = new LinkedList<Thread>();
            threads.AddLast(new Thread(this.Hello));
            threads.AddLast(new Thread(this.Hello));
            int index = 0;
            foreach (Thread t in threads)
            {
                t.Name = String.Format("Thread {0}", index++);
                t.Start();
            }
            while (threads.Count > 0)
            {
                Console.ReadLine();
                exitThread = threads.First.Value;
                exitThread.Join();
                threads.RemoveFirst();
            }
        }
        private void Hello()
        {
            Random rnd = new Random();
            while (true)
            {
                Console.WriteLine("{0} aktiv: {1}", Thread.CurrentThread.Name, rnd.Next());
                Thread.Sleep(rnd.Next(4000));
                if (exitThread == Thread.CurrentThread)
                {
                    Console.WriteLine("Ende: {0}", Thread.CurrentThread.Name);
                    return;
                }
            }
        }
    }
}
