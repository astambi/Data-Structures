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

        var longestSeq = GetLongestSequence(numbers);

        Console.WriteLine(string.Join(" ", longestSeq));
    }

    private static IEnumerable<int> GetLongestSequence(List<int> numbers)
    {
        var maxIndex = -1;
        var maxLen = 0;
        var start = 0;

        while (start <= numbers.Count - 1)
        {
            var currentLen = 1;

            for (int end = start + 1; end < numbers.Count; end++)
            {
                if (numbers[start] != numbers[end])
                {
                    break;
                }

                currentLen++;
            }

            if (currentLen > maxLen)
            {
                maxLen = currentLen;
                maxIndex = start;
            }

            start += currentLen;
        }

        return numbers
            .Skip(maxIndex)
            .Take(maxLen);
    }
}
