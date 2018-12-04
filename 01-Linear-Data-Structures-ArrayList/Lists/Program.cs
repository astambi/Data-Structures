using System;

public class Program
{
    public static void Main(string[] args)
    {
        var list = new ArrayList<int>();

        list.Add(-100);
        list.Add(200);
        list.Add(300);
        list.Add(400);
        Print(list);

        list[0] = list[0] * -1;
        Print(list);

        try
        {
            for (int i = 0; i < 2; i++)
            {
                var element = list.RemoveAt(list.Count - 1);
                Console.WriteLine($"Removed: {element}");
                Print(list);

                element = list.RemoveAt(0);
                Console.WriteLine($"Removed: {element}");
                Print(list);
            }

            var invalidElem = list.RemoveAt(100);
            Console.WriteLine($"Removed: {invalidElem}");
            Print(list);
        }
        catch (Exception)
        {
            Console.WriteLine("Error: Index out of range");
        }
    }

    private static void Print(ArrayList<int> list)
    {
        Console.WriteLine($"Count: {list.Count}");
        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine(list[i]);
        }
    }
}
