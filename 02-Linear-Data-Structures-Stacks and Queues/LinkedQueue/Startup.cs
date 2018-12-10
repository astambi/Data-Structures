using System;

public class Startup
{
    public static void Main()
    {
        var queue = new LinkedQueue<int>();
        for (int i = 1; i <= 5; i++)
        {
            queue.Enqueue(i);
        }

        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine($"Count {queue.Count}");
        Console.WriteLine("==============");

        for (int i = 1; i <= 7; i++)
        {
            try
            {
                Console.WriteLine($"Removed {queue.Dequeue()}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
            }

            Console.WriteLine(string.Join(", ", queue.ToArray()));
            Console.WriteLine($"Count {queue.Count}");
            Console.WriteLine("==============");
        }
    }
}

