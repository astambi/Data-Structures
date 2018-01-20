namespace LongestSubsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            var longestSequence = GetLongestSequence(numbers);

            if (longestSequence != null)
            {
                Console.WriteLine(string.Join(" ", longestSequence));
            }
        }

        private static IEnumerable<int> GetLongestSequence(List<int> numbers)
        {
            if (!numbers.Any())
            {
                return null;
            }

            var startIndex = 0;
            var maxLength = 1;

            for (int i = 0; i < numbers.Count - 1; i++)
            {
                var currentLength = 1;
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[i] == numbers[j])
                    {
                        currentLength++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (currentLength > maxLength)
                {
                    maxLength = currentLength;
                    startIndex = i;
                }
            }

            return numbers
                .Skip(startIndex)
                .Take(maxLength);
        }
    }
}
