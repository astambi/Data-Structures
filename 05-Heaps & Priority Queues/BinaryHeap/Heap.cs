using System;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        ConstructHeap(arr);
        Console.WriteLine($"Heap: {string.Join(" ", arr)}");

        SortHeap(arr);
    }

    private static void SortHeap(T[] arr)
    {
        for (int i = arr.Length - 1; i > 0; i--)
        {
            Swap(arr, 0, i);
            HeapifyDownIterative(arr, 0, i);
            //HeapifyDownRecursive(arr, 0, i);
        }
    }

    private static void ConstructHeap(T[] arr)
    {
        for (int i = arr.Length / 2; i >= 0; i--)
        {
            HeapifyDownIterative(arr, i, arr.Length);
            //HeapifyDownRecursive(arr, i, arr.Length);
        }
    }

    private static void HeapifyDownIterative(T[] arr, int current, int length)
    {
        while (current < length / 2)
        {
            var maxChild = 2 * current + 1;
            if (maxChild < length - 1
                && IsGreaterThan(arr, maxChild + 1, maxChild))
            {
                maxChild++;
            }

            if (IsGreaterThan(arr, current, maxChild))
            {
                break;
            }

            Swap(arr, current, maxChild);
            current = maxChild;
        }
    }

    private static void HeapifyDownRecursive(T[] arr, int current, int length)
    {
        var maxChild = 2 * current + 1;
        if (maxChild > length - 1)
        {
            return;
        }

        if (maxChild < length - 1
            && IsGreaterThan(arr, maxChild + 1, maxChild))
        {
            maxChild++;
        }

        if (IsGreaterThan(arr, maxChild, current))
        {
            Swap(arr, current, maxChild);
            HeapifyDownRecursive(arr, maxChild, length);
        }
    }

    private static bool IsGreaterThan(T[] arr, int current, int other)
        => arr[current].CompareTo(arr[other]) > 0;

    private static void Swap(T[] arr, int current, int other)
    {
        var temp = arr[other];
        arr[other] = arr[current];
        arr[current] = temp;
    }
}
