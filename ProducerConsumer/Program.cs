using System;
using System.Threading;

namespace Threading
{
    class ProducerConsumer
    {
        static void Main(string[] args)
        {
            Item item = new Item();
            Producer producer = new Producer();
            Consumer consumer = new Consumer();
            Thread producerThread = new Thread(producer.Produce);
            Thread consumerThread = new Thread(consumer.Consume);
            producerThread.Start(item);
            consumerThread.Start(item);
        }
    }
    internal class Producer
    {
        public void Produce(object i)
        {
            Item item = (Item)i;
            int generation = 0;
            Random rnd = new Random();
            while (true)
            {
                Monitor.Enter(item.used);
                if (item.data != String.Empty)
                    Monitor.Wait(item.used);
                item.data = String.Format("Ware #{0} ", generation++);
                Thread.Sleep(rnd.Next(1000));
                Console.WriteLine(" Producer erzeugt {0}", item.data);
                Monitor.Pulse(item.used);
                Monitor.Exit(item.used);
            }
        }
    }
    internal class Consumer
    {
        public void Consume(object i)
        {
            Item item = (Item)i;
            while (true)
            {
                Monitor.Enter(item.used);
                if (item.data == String.Empty)
                    Monitor.Wait(item.used);
                Console.WriteLine(" Consumer bekommt {0}", item.data);
                item.data = String.Empty;
                Monitor.Pulse(item.used);
                Monitor.Exit(item.used);
            }
        }
    }
    internal class Item
    {
        public object used = new object();
        public string data = String.Empty;
    }
}


