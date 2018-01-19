namespace SumAndAverage
{
    using System;
    using System.Linq;

    public class Startup
    {
        private const string Stats = "Sum={0}; Average={1:F2}";
        private const int DefaultValue = 0;

        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            if (numbers.Any())
            {
                Console.WriteLine(string.Format(Stats, numbers.Sum(), numbers.Average()));
            }
            else
            {
                Console.WriteLine(string.Format(Stats, DefaultValue, DefaultValue));
            }
        }
    }
}
