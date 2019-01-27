using System;

public class Program
{
    public static void Main()
    {
        var tree = GetTree();

        PrintSearchAny(tree, 12, 19);
        PrintSearchAny(tree, 9, 50);
        PrintSearchAny(tree, 102, 119);
        PrintSearchAny(tree, 99, 100);
        PrintSearchAny(tree, 98.9, 100);
        PrintSearchAny(tree, -5, 0);
        PrintSearchAny(tree, -5, 0.1);

        PrintSearchAll(tree, 9, 50);
        PrintSearchAll(tree, 20, 30);
        PrintSearchAll(tree, 15, 20);
    }

    private static IntervalTree GetTree()
    {
        var tree = new IntervalTree();
        tree.Insert(20, 36);
        tree.Insert(3, 10);
        tree.Insert(0, 1);
        tree.Insert(10, 15);
        tree.Insert(29, 99);
        tree.Insert(25, 30);
        tree.Insert(60, 72);

        return tree;
    }

    private static void PrintSearchAny(IntervalTree tree, double lo, double hi)
        => Console.WriteLine($"Search Any [{lo}, {hi}]: {tree.SearchAny(lo, hi)}");

    private static void PrintSearchAll(IntervalTree tree, double lo, double hi)
    {
        Console.WriteLine($"Search All [{lo}, {hi}]:");
        var result = tree.SearchAll(lo, hi);

        foreach (var interval in result)
        {
            Console.WriteLine(interval);
        }
    }
}
