namespace Rope
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using Wintellect.PowerCollections;

    public class Startup
    {
        private const int iterations = 100000;

        public static void Main()
        {
            Console.WriteLine($"Iterations: {iterations}");

            RopeAppendTimer();
            RopePrependTimer();
            RopeMiddleTimer();

            StringBuilderAppendTimer();
            StringBuilderPrependTimer();
            StringBuilderMiddleTimer();
        }

        private static void RopeAppendTimer()
        {
            var rope = new BigList<string>();
            var timer = NewTimer();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                rope.Add(i.ToString());
            }

            Print(timer, "Rope Append");
        }

        private static void RopePrependTimer()
        {
            var rope = new BigList<string>();
            var timer = NewTimer();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                rope.Insert(0, i.ToString());
            }

            Print(timer, "Rope Prepend");
        }

        private static void RopeMiddleTimer()
        {
            var rope = new BigList<string>();
            var timer = NewTimer();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                rope.Insert(rope.Count / 2, i.ToString());
            }

            Print(timer, "Rope Middle");
        }

        private static void StringBuilderAppendTimer()
        {
            var builder = new StringBuilder();
            var timer = NewTimer();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                builder.Append(i.ToString());
            }

            Print(timer, "StringBuilder Append");
        }

        private static void StringBuilderPrependTimer()
        {
            var builder = new StringBuilder();
            var timer = NewTimer();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                builder.Insert(0, i.ToString());
            }

            Print(timer, "StringBuilder Prepend");
        }

        private static void StringBuilderMiddleTimer()
        {
            var builder = new StringBuilder();
            var timer = NewTimer();
            timer.Start();

            for (int i = 0; i < iterations; i++)
            {
                builder.Insert(builder.Length / 2, i.ToString());
            }

            Print(timer, "StringBuilder Middle");
        }

        private static Stopwatch NewTimer()
            => new Stopwatch();

        private static void Print(Stopwatch timer, string message)
        {
            timer.Stop();
            Console.WriteLine($"{timer.Elapsed} - {message}");
        }
    }
}
