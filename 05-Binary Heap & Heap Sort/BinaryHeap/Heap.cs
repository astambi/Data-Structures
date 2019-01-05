using System;

public static class Heap<T> where T : IComparable<T>
{
    // O(n logN) 
    public static void Sort(T[] arr)
    {
        BuildMaxHeap(arr);
        SortHeap(arr);
    }

    private static void SortHeap(T[] arr)
    {
        for (int i = arr.Length - 1; i > 0; i--)
        {
            Swap(0, i, arr);  // Swap first (Max) with last unsorted elem 
            HeapifyDownRecursive(0, i - 1, arr);  // Heapify the new first elem down the unsorted arr
        }
    }

    private static void BuildMaxHeap(T[] arr)
    {
        for (int i = arr.Length / 2; i >= 0; i--) // heapify parents only
        {
            HeapifyDownRecursive(i, arr.Length - 1, arr);
        }
    }

    private static void HeapifyDownRecursive(int parentIndex, int length, T[] arr)
    {
        var maxChildIndex = GetMaxChild(parentIndex, length, arr);

        if (IsValid(maxChildIndex, length)
            && IsGreater(maxChildIndex, parentIndex, arr)) // child > parent
        {
            Swap(parentIndex, maxChildIndex, arr);
            HeapifyDownRecursive(maxChildIndex, length, arr);
        }
    }

    private static bool IsGreater(int indexA, int indexB, T[] arr) // A > B
        => arr[indexA].CompareTo(arr[indexB]) > 0;

    private static bool IsValid(int index, int length)
        => 0 <= index && index <= length;

    private static int GetLeftChild(int parentIndex) 
        => parentIndex * 2 + 1;

    private static int GetMaxChild(int parentIndex, int length, T[] arr)
    {
        var leftChildIndex = GetLeftChild(parentIndex);
        if (!IsValid(leftChildIndex, length))
        {
            return -1;
        }

        var rightChildIndex = leftChildIndex + 1;
        if (!IsValid(rightChildIndex, length))
        {
            return leftChildIndex;
        }

        return IsGreater(rightChildIndex, leftChildIndex, arr) // right > left
            ? rightChildIndex
            : leftChildIndex;
    }

    private static void Swap(int indexA, int indexB, T[] arr)
    {
        var temp = arr[indexA];
        arr[indexA] = arr[indexB];
        arr[indexB] = temp;
    }
}
