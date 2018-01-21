using System;

public class Startup
{
    public static void Main()
    {
        var linkedQueue = new LinkedQueue<int>();

        for (int i = 0; i < 10; i++)
        {
            linkedQueue.Enqueue(i);
            Console.WriteLine($"Enqueued element: {i}");
        }

        Console.WriteLine($"Count: {linkedQueue.Count}");
        Console.WriteLine(string.Join(" ", linkedQueue.ToArray()));

        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Dequeued element: {linkedQueue.Dequeue()}");
        }

        Console.WriteLine($"Count: {linkedQueue.Count}");
        Console.WriteLine(string.Join(" ", linkedQueue.ToArray()));

        foreach (var element in linkedQueue)
        {
            Console.WriteLine($"Element: {element}");
        }
    }
}
