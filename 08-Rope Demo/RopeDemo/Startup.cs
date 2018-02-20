using System;
using System.Diagnostics;
using System.Text;
using Wintellect.PowerCollections;

namespace RopeDemo
{
    public class Startup
    {
        public static void Main()
        {
            var iterations = 100000;

            RopeAppendTimer(iterations);
            StringBuilderAppendTimer(iterations);

            RopeInsertTimer(iterations);
            StringBuilderInsertTimer(iterations);
        }

        private static void RopeAppendTimer(int iterations)
        {
            var rope = new BigList<string>();
            var timer = NewTimer();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                rope.Add(i.ToString());
            }

            timer.Stop();
            Console.WriteLine($"{timer.Elapsed} - Rope Add");
        }

        private static void StringBuilderAppendTimer(int iterations)
        {
            var builder = new StringBuilder();
            var timer = NewTimer();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                builder.Append(i.ToString());
            }

            timer.Stop();
            Console.WriteLine($"{timer.Elapsed} - StringBuilder Append");
        }

        private static void RopeInsertTimer(int iterations)
        {
            var rope = new BigList<string>();
            var timer = NewTimer();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                rope.Insert(0, i.ToString());
            }

            timer.Stop();
            Console.WriteLine($"{timer.Elapsed} - Rope Insert at 0");
        }

        private static void StringBuilderInsertTimer(int iterations)
        {
            var builder = new StringBuilder();
            var timer = NewTimer();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                builder.Insert(0, i.ToString());
            }

            timer.Stop();
            Console.WriteLine($"{timer.Elapsed} - StringBuilder Insert at 0");
        }

        private static Stopwatch NewTimer()
            => new Stopwatch();
    }
}
