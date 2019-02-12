using System;

public class Program
{
    public static void Main()
    {
        QuadTree<TestBox> quadTree = new QuadTree<TestBox>(200, 200, 5);
        var source = new TestBox(0, 0);

        quadTree.Insert(source);
        quadTree.Insert(new TestBox(0, 10));
        quadTree.Insert(new TestBox(-10, -10, 200, 200));
        quadTree.Insert(new TestBox(5, 5));
        quadTree.Insert(new TestBox(-10, -10, 150, 150));

        var collisions = quadTree.Report(source.Bounds);

        foreach (var collision in collisions)
        {
            Console.WriteLine(collision);
        }
    }
}
