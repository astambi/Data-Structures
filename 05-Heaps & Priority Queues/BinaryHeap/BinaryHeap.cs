using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count => this.heap.Count;

    public void Insert(T item)
    {
        this.heap.Add(item);

        this.HeapifyUpIterative(this.Count - 1);
        //this.HeapifyUpRecursive(this.Count - 1);
    }

    public T Peek()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return this.heap[0];
    }

    public T Pull()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var max = this.heap[0];
        this.Swap(0, this.Count - 1);
        this.heap.RemoveAt(this.Count - 1);

        this.HeapifyDownIterative(0);
        //this.HeapifyDownRecursive(0);

        return max;
    }

    private void HeapifyUpIterative(int current)
    {
        while (current > 0
            && this.IsGreaterThan(current, (current - 1) / 2))
        {
            this.Swap(current, (current - 1) / 2);
            current = (current - 1) / 2;
        }
    }

    private void HeapifyUpRecursive(int current)
    {
        var parent = (current - 1) / 2;

        if (this.IsGreaterThan(current, parent))
        {
            this.Swap(current, parent);
            this.HeapifyUpRecursive(parent);
        }
    }

    private void HeapifyDownIterative(int current)
    {
        while (current < this.Count / 2)
        {
            var maxChild = 2 * current + 1;

            if (maxChild < this.Count - 1
                && this.IsGreaterThan(maxChild + 1, maxChild))
            {
                maxChild++;
            }

            if (this.IsGreaterThan(current, maxChild))
            {
                break;
            }

            this.Swap(current, maxChild);
            current = maxChild;
        }
    }

    private void HeapifyDownRecursive(int current)
    {
        var maxChild = 2 * current + 1;
        if (maxChild > this.Count - 1)
        {
            return;
        }

        if (maxChild < this.Count - 1
            && this.IsGreaterThan(maxChild + 1, maxChild))
        {
            maxChild++;
        }

        if (this.IsGreaterThan(maxChild, current))
        {
            this.Swap(current, maxChild);
            this.HeapifyDownRecursive(maxChild);
        }
    }

    private void Swap(int current, int parent)
    {
        var oldParent = this.heap[parent];
        this.heap[parent] = this.heap[current];
        this.heap[current] = oldParent;
    }

    private bool IsGreaterThan(int first, int second)
        => this.heap[first].CompareTo(this.heap[second]) > 0;
}
