using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace laboratorium3_Schmid_Galecki
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 10;
            Test test = new Test();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Thread[] threads = new Thread[n];
            for (int i = 0; i < n; i++)
            {
                var temp = i;
                threads[i] = new Thread(() => test.set(temp));
            }
            for (int i = 0; i < n; i++)
            {
                threads[i].Start();
            }
            for (int i = 0; i < n; i++)
                threads[i].Join();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs + " ms");
            foreach (double x in test.array)
                Console.WriteLine(x + " ");
            Console.Read();
        }
        public class Test
        {
            public double[] array = new double[10];
            public void set(int i)
            {
                Thread.Sleep(500);
                double x = Math.Sqrt((int)i);
                array[(int)i] = x;
            }
        }

    }
}
