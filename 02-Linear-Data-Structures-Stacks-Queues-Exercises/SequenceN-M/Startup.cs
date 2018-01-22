using System;
using System.Collections.Generic;
using System.Linq;

public class Startup
{
    public static void Main()
    {
        var numbers = Console.ReadLine()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
        var startValue = numbers[0];
        var resultValue = numbers[1];

        var solutionItem = FindSolution(startValue, resultValue);

        PrintSolution(solutionItem);
    }

    private static Item FindSolution(int startValue, int resultValue)
    {
        var items = new Queue<Item>();
        items.Enqueue(new Item(startValue, null));

        while (items.Count > 0)
        {
            var currentItem = items.Dequeue();

            if (currentItem.Value < resultValue)
            {
                items.Enqueue(new Item(currentItem.Value + 1, currentItem));
                items.Enqueue(new Item(currentItem.Value + 2, currentItem));
                items.Enqueue(new Item(currentItem.Value * 2, currentItem));
            }
            else if (currentItem.Value == resultValue)
            {
                return currentItem;
            }
        }

        return null;
    }

    private static void PrintSolution(Item resultItem)
    {
        var operations = new Stack<int>();
        while (resultItem != null)
        {
            operations.Push(resultItem.Value);
            resultItem = resultItem.PreviousItem;
        }

        if (operations.Count > 0)
        {
            Console.WriteLine(string.Join(" -> ", operations));
        }
    }

    private class Item
    {
        public Item(int value, Item previousItem)
        {
            this.Value = value;
            this.PreviousItem = previousItem;
        }

        public int Value { get; private set; }

        public Item PreviousItem { get; private set; }
    }
}
