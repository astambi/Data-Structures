using System;

public class Program
{
    public static void Main()
    {
        AVL<int> tree = new AVL<int>();
        tree.Insert(1);
        tree.Insert(3);
        tree.Insert(2);

        Console.WriteLine();
    }
}
