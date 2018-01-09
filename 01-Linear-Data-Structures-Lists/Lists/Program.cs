using System;

public class Program
{
    public static void Main(string[] args)
    {
        var collection = new ArrayList<int>();

        collection.Add(10);
        Console.WriteLine(collection.Count);

        var element = collection.RemoveAt(0);
        Console.WriteLine(element);
        Console.WriteLine(collection.Count);
    }
}
