using System;

public class HeapExample
{
    static void Main()
    {
        Console.WriteLine("Created an empty heap.");
        var heap = new BinaryHeap<int>();
        heap.Insert(5);
        heap.Insert(8);
        heap.Insert(1);
        heap.Insert(3);
        heap.Insert(12);
        heap.Insert(-4);

        Console.WriteLine("Heap elements (max to min):");
        while (heap.Count > 0)
        {
            var max = heap.Pull();
            Console.WriteLine(max);
        }

        var array = new int[] { 8, 5, 7, 1, 3, 2, 4, 9 };
        Console.WriteLine($"Unsorted array: {string.Join(" ", array)}");

        Heap<int>.Sort(array);
        Console.WriteLine($"Sorted array: {string.Join(" ", array)}");
    }
}
