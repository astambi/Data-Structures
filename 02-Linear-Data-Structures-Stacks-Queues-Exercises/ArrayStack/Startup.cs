using System;

public class Startup
{
    public static void Main()
    {
        var arrayStack = new ArrayStack<int>();
        for (int i = 0; i < 20; i++)
        {
            arrayStack.Push(i);
        }

        Console.WriteLine($"Count: {arrayStack.Count}");
        Console.WriteLine(string.Join(" ", arrayStack.ToArray()));

        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"Popped element: {arrayStack.Pop()}");
        }

        Console.WriteLine($"Count: {arrayStack.Count}");
        Console.WriteLine(string.Join(" ", arrayStack.ToArray()));
    }
}
