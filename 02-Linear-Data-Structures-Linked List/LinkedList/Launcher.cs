using System;

public class Launcher
{
    private const int Count = 5;

    public static void Main()
    {
        var linkedList = new LinkedList<int>();

        for (int i = 1; i <= Count; i++)
        {
            linkedList.AddFirst(-i);
            linkedList.AddLast(i);
        }

        for (int i = 0; i < Count; i++)
        {
            linkedList.RemoveFirst();
            linkedList.RemoveLast();

            foreach (var item in linkedList)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine($"Count: {linkedList.Count}");
            Console.WriteLine("================");
        }

        try
        {
            linkedList.RemoveFirst();
            linkedList.RemoveLast();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}
