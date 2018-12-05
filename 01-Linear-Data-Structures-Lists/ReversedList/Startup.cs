using System;

public class Program
{
    public static void Main()
    {
        var reversedList = new ReversedList<int>();

        // Add
        reversedList.Add(100);
        reversedList.Add(200);
        reversedList.Add(300);
        reversedList.Add(-400);

        // Indexer
        Console.WriteLine(reversedList[0]);
        reversedList[0] *= -1;
        Console.WriteLine(reversedList[0]);

        Print(reversedList);

        // RemoveAt
        for (int i = 0; i < 2; i++)
        {
            Console.WriteLine($"Removed: {reversedList.RemoveAt(reversedList.Count - 1)}");
            Print(reversedList);

            Console.WriteLine($"Removed: {reversedList.RemoveAt(0)}");
            Print(reversedList);
        }
    }

    private static void Print(ReversedList<int> reversedList)
    {
        Console.WriteLine($"Capacity: {reversedList.Capacity}");
        Console.WriteLine($"Count: {reversedList.Count}");

        foreach (var item in reversedList)
        {
            Console.WriteLine(item);
        }
    }
}
