using System;

public class Launcher
{
    public static void Main()
    {
        var linkedList = new LinkedList<int>();

        for (int i = 1; i < 10; i++)
        {
            linkedList.AddLast(i);
            linkedList.AddFirst(-i);
        }

        PrintList(linkedList);

        for (int i = 1; i < 5; i++)
        {
            linkedList.RemoveFirst();
        }

        PrintList(linkedList);

        for (int i = 1; i < 5; i++)
        {
            linkedList.RemoveLast();
        }

        PrintList(linkedList);
    }

    private static void PrintList(LinkedList<int> linkedList)
    {
        Console.WriteLine("List:");
        foreach (var item in linkedList)
        {
            Console.WriteLine(item);
        }
    }
}
