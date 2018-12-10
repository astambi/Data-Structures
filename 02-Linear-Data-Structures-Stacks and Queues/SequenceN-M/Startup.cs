using System;
using System.Collections.Generic;
using System.Linq;

public class Startup
{
    public static void Main()
    {
        var input = Console.ReadLine()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
        var start = input[0];
        var result = input[1];

        if (start > result)
        {
            return;
        }

        FindSolution(start, result);
    }

    private static void PrintSolution(Item item, int start)
    {
        var solution = new Stack<int>();
        while (item != null)
        {
            solution.Push(item.Value);
            item = item.Previous;
        }

        Console.WriteLine(string.Join(" -> ", solution));
    }

    private static void FindSolution(int start, int result)
    {
        var queue = new Queue<Item>();
        queue.Enqueue(new Item(start, null));

        while (queue.Any())
        {
            var current = queue.Dequeue();

            if (current.Value == result)
            {
                PrintSolution(current, start);
                break;
            }

            if (current.Value < result)
            {
                queue.Enqueue(new Item(current.Value + 1, current));
                queue.Enqueue(new Item(current.Value + 2, current));
                queue.Enqueue(new Item(current.Value * 2, current));
            }
        }
    }

    private class Item
    {
        public Item(int value, Item previousItem)
        {
            this.Value = value;
            this.Previous = previousItem;
        }

        public int Value { get; }

        public Item Previous { get; }
    }
}
