using System;
using System.Threading;

namespace Threading
{
    class HelloWorld    
    {
        static void Main(string[] args)
        {
            HelloWorld ht = new HelloWorld();
            Thread t1 = new Thread(ht.Hello);
            t1.Name = "Hello Thread A";
            t1.Start();
            Console.ReadLine();
            t1.Abort();
        }
        private void Hello()
        {
            Random rnd = new Random ();
            while (true)
            {
                Console.Out.WriteLine(Thread.CurrentThread.Name);
                Thread.Sleep(rnd.Next(4000));
            }
        }
    }
}
