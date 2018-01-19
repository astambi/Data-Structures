namespace RemoveOddOccurrences
{
    using System;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            var evenOccurrencesSeq = numbers
                .Where(n => numbers.Count(e => e == n) % 2 == 0)
                .ToList();

            Console.WriteLine(string.Join(" ", evenOccurrencesSeq));
        }
    }
}
