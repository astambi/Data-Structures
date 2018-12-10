using System;
using System.Collections.Generic;

public class Startup
{
    private const int Count = 50;

    public static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        var sequence = CalculateSequence(n);
        Console.WriteLine(string.Join(", ", sequence));
    }

    private static int[] CalculateSequence(int n)
    {
        var queue = new Queue<int>();
        queue.Enqueue(n);

        var sequence = new int[Count];
        var index = 0;
        sequence[index++] = n;

        while (index < Count)
        {
            var current = queue.Dequeue();
            var first = current + 1;
            var second = 2 * current + 1;
            var third = current + 2;

            try
            {
                sequence[index++] = first;
                sequence[index++] = second;
                sequence[index++] = third;

                queue.Enqueue(first);
                queue.Enqueue(second);
                queue.Enqueue(third);
            }
            catch (Exception)
            {
                break;
            }
        }

        return sequence;
    }
}
