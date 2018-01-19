namespace CountOfOccurrences
{
    using System;
    using System.Linq;
    using System.Text;

    public class Startup
    {
        private const string Message = "{0} -> {1} times";

        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            var uniqueNumbers = numbers
                .Distinct()
                .OrderBy(n => n)
                .ToList();

            var builder = new StringBuilder();
            foreach (var number in uniqueNumbers)
            {
                builder.AppendLine(string.Format(
                    Message,
                    number,
                    numbers.Count(n => n == number)));
            }

            Console.WriteLine(builder.ToString().Trim());
        }
    }
}
