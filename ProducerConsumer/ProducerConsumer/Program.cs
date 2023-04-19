using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProducerConsumer
{
    internal class Program
    {
        static Queue<object> something = new Queue<object>();

        static void Main(string[] args)
        {

            Thread c1 = new Thread(new ThreadStart(GetSomething));
            c1.Start();

            Thread p1 = new Thread(new ThreadStart(RestockSomething));
            p1.Start();

            Console.ReadLine();
        }
        static public void GetSomething()
        {
            while (true)
            {

                lock (something)
                {
                    Thread.Sleep(100);
                    if (something.Count < 2)
                    {
                        Monitor.Wait(something);
                    }
                    something.Dequeue();

                    Console.WriteLine(something.Count + " Count | Consumed");
                }
            }
        }
        static public void RestockSomething()
        {
            while (true)
            {

                lock (something)
                {
                    int some = 1;
                    Thread.Sleep(50);
                    something.Enqueue(some);

                    Console.WriteLine(something.Count + " Count | produced");

                    Monitor.PulseAll(something);
                }
            }
        }
    }
}