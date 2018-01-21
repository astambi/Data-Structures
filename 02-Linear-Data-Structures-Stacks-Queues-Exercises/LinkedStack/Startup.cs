using System;

public class Startup
{
    public static void Main()
    {
        var linkedStack = new LinkedStack<int>();

        for (int i = 0; i < 10; i++)
        {
            linkedStack.Push(i);
        }

        Console.WriteLine($"Count: {linkedStack.Count}");
        Console.WriteLine(string.Join(" ", linkedStack.ToArray()));

        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Popped element: {linkedStack.Pop()}");
        }

        Console.WriteLine($"Count: {linkedStack.Count}");
        Console.WriteLine(string.Join(" ", linkedStack.ToArray()));
    }
}
