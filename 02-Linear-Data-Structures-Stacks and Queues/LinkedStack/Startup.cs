using System;

public class Startup
{
    private const int Count = 5;

    // TODO Time limit Judge
    public static void Main()
    {
        var linkedStack = new LinkedStack<int>();

        for (int i = 0; i < Count; i++)
        {
            linkedStack.Push(i);
        }

        //foreach (var item in linkedStack)
        //{
        //    Console.WriteLine(item);
        //}

        Console.WriteLine($"Count: {linkedStack.Count}");
        //Console.WriteLine($"Top: {linkedStack.Peek()}");
        Console.WriteLine("============");

        Console.WriteLine(string.Join(", ", linkedStack.ToArray()));
        Console.WriteLine("============");

        try
        {
            for (int i = 0; i <= Count; i++)
            {
                Console.WriteLine($"Popped: {linkedStack.Pop()}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }

        Console.WriteLine($"Count: {linkedStack.Count}");
        Console.WriteLine("============");

        //try
        //{
        //    Console.WriteLine($"Top: {linkedStack.Peek()}");
        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine($"Error: {e.Message}");
        //}
    }
}

