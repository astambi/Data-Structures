namespace LongestSubsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Startup
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            if (!numbers.Any())
            {
                return;
            }

            //var longestSeq = GetLongestSequenceOfEqualNumbersWithList(numbers);
            var longestSeq = GetLongestSequenceFromStartIndexAndLength(numbers);

            Console.WriteLine(string.Join(" ", longestSeq));
        }

        private static List<int> GetLongestSequenceOfEqualNumbersWithList(List<int> numbers)
        {
            var longestSeq = new List<int> { numbers[0] };
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                var currentSeq = new List<int> { numbers[i] };
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[i] == numbers[j])
                    {
                        currentSeq.Add(numbers[j]);
                    }
                    else
                    {
                        break;
                    }
                }

                if (currentSeq.Count > longestSeq.Count)
                {
                    longestSeq = currentSeq;
                }
            }

            return longestSeq;
        }

        private static string GetLongestSequenceFromStartIndexAndLength(List<int> numbers)
        {
            var longestSeqParams = GetSeqStartIndexAndLength(numbers);
            var startIndex = longestSeqParams[0];
            var seqLength = longestSeqParams[1];

            var builder = new StringBuilder();
            for (int i = 0; i < seqLength; i++)
            {
                builder.Append($"{numbers[startIndex]} ");
            }

            return builder.ToString().Trim();
        }


        private static int[] GetSeqStartIndexAndLength(List<int> numbers)
        {
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

            return new[] { startIndex, maxLength };
        }
    }
}
