using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace MultiThreadingTest
{
    class MultiThreadingTest
    {
        int Size;
        int Complexity;
        int Threads;
        double[] a;
        double[] b;

        public MultiThreadingTest(int Size, int Complexity, int Threads)
        {
            this.Threads = Threads;
            this.Size = Size;
            this.Complexity = Complexity;
            a = new double[Size];
            b = new double[Size];
            Random rnd = new Random();
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = rnd.Next(1000, 2000);
            }
        }

        void Input(object BeginEnd)
        {
            string[] str = BeginEnd as string[];
            for (int i = int.Parse(str[0]); i < int.Parse(str[1]); i++)
            {
                for (int j = 0; j < Complexity; j++) b[i] += Math.Pow(a[i], 2);//1.789
            }
        }

        void StartThreads(int Number)
        {
            int begin = 0;
            int end = Size / Number;
            string[] str = new string[2];
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < Number; i++)
            {
                threads.Add(new Thread(new ParameterizedThreadStart(Input)));
            }

            foreach (Thread thrd in threads)
            {
                str[0] = string.Format("{0}", begin);
                str[1] = string.Format("{0}", end);
                thrd.Start(str as object);
                begin += Size / Number;
                end += Size / Number;
                Thread.Sleep(10);
            }

            foreach (Thread thrd in threads)
            {
                thrd.Join();
            }
            Array.Clear(b, 0, Size);
        }

        public int MediumTime(int NumberOfThreads)
        {
            int AllTimeMiliseconds = 0;
            for (int n = 0; n < 10; n++)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                StartThreads(NumberOfThreads);
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                AllTimeMiliseconds += ts.Milliseconds;
            }
            return AllTimeMiliseconds / 10;
        }

        public int MediumTimeConstThreads(int Complexity)
        {
            this.Complexity = Complexity;
            int AllTimeMiliseconds = 0;
            for (int n = 0; n < 10; n++)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                StartThreads(Threads);
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                AllTimeMiliseconds += ts.Milliseconds;
            }
            return AllTimeMiliseconds / 10;
        }
    }
}
