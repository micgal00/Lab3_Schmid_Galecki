using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace lab3_mnozenie_macierzy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = 1000;
            int liczba_watkow = 5;
            Thread[] threads = new Thread[liczba_watkow];

            Matrix macierz1 = new Matrix(size);
            Matrix macierz2 = new Matrix(size);
            Matrix wynik = new Matrix(size);
            Matrix wynikThr = new Matrix(size);

            macierz1.fillMatrix(20);
            //macierz1.printMatrix();
            //Console.WriteLine("\n\n");

            macierz2.fillMatrix(20);
            //macierz2.printMatrix();
            //Console.WriteLine("\n\n");

            var watchThr = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < liczba_watkow; i++)
            {
                var temp = i;
                threads[i] = new Thread(() => macierz1.multiplyMatrixThreads(temp, macierz2, wynikThr));
            }

            for (int i = 0; i < liczba_watkow; i++)
            {
                threads[i].Start();
            }

            for (int i = 0; i < liczba_watkow; i++)
            {
                threads[i].Join();
            }

            watchThr.Stop();
            var elapsedMsThr = watchThr.ElapsedMilliseconds;
            Console.WriteLine("\n\nMnozenie watkami: " + elapsedMsThr + "ms");

            var watch = System.Diagnostics.Stopwatch.StartNew();

            macierz1.multiplyMatrixIn(macierz2, wynik);

            watch.Stop();

            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("\n\nMnozenie zwykle: " + elapsedMs + "ms");

            //wynik.printMatrix();

            Console.WriteLine("\n\n");
            //wynikThr.printMatrix();

            Console.Read();

        }
        public class Matrix
        {
            public int matrixSize { get; set; }
            public double[,] array = new double[10,10];

            public Matrix(int size)
            {
                array = new double[size, size];
                matrixSize = size;
            }
            public void fillMatrix(int range)
            {
                Random r = new Random();

                for (int ki = 0; ki < matrixSize; ki++)
                {
                    for (int kj = 0; kj < matrixSize; kj++)
                    {
                        array[ki, kj] = r.Next(1, range);
                    }
                }
            }
            public void printMatrix()
            {
                for (int i = 0; i < matrixSize; i++)
                {
                    for (int j = 0; j < matrixSize; j++)
                    {
                        Console.Write(array[i, j] + " ");
                    }
                    Console.WriteLine("\n");
                }
            }
            public void multiplyMatrixIn(Matrix mat2, Matrix result)
            {
                for (int i = 0; i < matrixSize; i++)
                {
                    for (int j = 0; j < matrixSize; j++)
                    {
                        double s = 0;
                        for (int k = 0; k < matrixSize; k++)
                        {
                            s += this.array[i, k] * mat2.array[k, j];
                        }
                        //result.array[i, j] = Math.Sqrt((int)s);
                        result.array[i, j] = s;
                    }
                }
            }
            public void multiplyMatrixThreads(int threadNr, Matrix mat2, Matrix result)
            {
                for (int i = threadNr * (matrixSize/5); i < (threadNr + 1) * (matrixSize/5); i++)
                {
                    for (int j = 0; j < matrixSize; j++)
                    {
                        double s = 0;
                        for (int k = 0; k < matrixSize; k++)
                        {
                            s += this.array[i, k] * mat2.array[k, j];
                        }
                        //result.array[i, j] = Math.Sqrt((int)s);
                        result.array[i, j] = s;
                    }
                }
            }
        }
    }
}
