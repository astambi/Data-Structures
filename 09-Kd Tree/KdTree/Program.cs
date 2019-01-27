using System;

public class Program
{
    public static void Main()
    {
        KdTree tree = new KdTree();
        tree.Insert(new Point2D(5, 5));
        tree.Insert(new Point2D(3, 2));
        tree.Insert(new Point2D(2, 6));
        tree.Insert(new Point2D(8, 8));
        tree.Insert(new Point2D(8, 9));
        tree.Insert(new Point2D(8, 9)); // no duplicates

        PrintContains(tree, new Point2D(5, 5));
        PrintContains(tree, new Point2D(8, 9));
        PrintContains(tree, new Point2D(0, 0));
    }

    private static void PrintContains(KdTree tree, Point2D point2D)
        => Console.WriteLine($"Contains {point2D}: {tree.Contains(point2D)}");
}
