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
        static Queue<object> Coffee = new Queue<object>();

        static void Main(string[] args)
        {

            Thread c1 = new Thread(new ThreadStart(GetCoffee));
            c1.Start();

            Thread p1 = new Thread(new ThreadStart(RestockCoffee));
            p1.Start();

            Console.ReadLine();
        }
        static public void GetCoffee()
        {
            while (true)
            {

                lock (Coffee)
                {
                    Thread.Sleep(100);
                    if (Coffee.Count < 2)
                    {
                        Monitor.Wait(Coffee);
                    }
                    Coffee.Dequeue();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Coffee.Count + " Count | Coffee Consumed");
                }
            }
        }
        static public void RestockCoffee()
        {
            while (true)
            {

                lock (Coffee)
                {
                    Thread.Sleep(100);
                    Coffee.Enqueue(1);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(Coffee.Count + " Count | Coffee produced");

                    Monitor.PulseAll(Coffee);
                }
            }
        }
    }
}