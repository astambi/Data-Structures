using System;

public class Startup
{
    public static void Main()
    {
        // .NetFramwork => .NetCore wont compile

        var collection = new ReversedList<int>();
        collection.Add(1);
        collection.Add(2);
        collection.Add(3);

        Console.WriteLine("---");
        Console.WriteLine(collection.RemoveAt(2));

        Console.WriteLine("---");
        Console.WriteLine(collection.Capacity);
        Console.WriteLine(collection.Count);

        Console.WriteLine("---");
        foreach (var item in collection)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("---");
    }
}
