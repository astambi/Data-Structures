using System;
using System.Collections.Generic;
using System.Linq;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap; // Max Heap (parent >= child)

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count => this.heap.Count;

    // O(log(n))
    public void Insert(T item)
    {
        this.heap.Add(item);

        //this.HeapifyUpIterative(this.Count - 1);
        this.HeapifyUpRecursive(this.Count - 1);
    }

    // O(1)
    public T Peek()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return this.heap[0];
    }

    // O(log(n))
    public T Pull()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var maxElement = this.heap[0];
        this.Swap(0, this.Count - 1); // swap Max with last
        this.heap = this.heap.Take(this.Count - 1).ToList(); // resize heap

        //this.HeapifyDownIterative(0);
        this.HeapifyDownRecursive(0);

        return maxElement;
    }

    private void HeapifyUpIterative(int childIndex)
    {
        while (true)
        {
            var parentIndex = this.GetParentIndex(childIndex);

            if (this.IsValid(parentIndex)
                && this.IsGreater(childIndex, parentIndex)) // child > parent
            {
                this.Swap(parentIndex, childIndex);
                childIndex = parentIndex;
            }
            else
            {
                break;
            }
        }
    }

    private void HeapifyUpRecursive(int childIndex)
    {
        var parentIndex = this.GetParentIndex(childIndex);

        if (this.IsValid(parentIndex)
            && this.IsGreater(childIndex, parentIndex)) // child > parent
        {
            this.Swap(parentIndex, childIndex);
            this.HeapifyUpRecursive(parentIndex);
        }
    }

    private void HeapifyDownIterative(int parentIndex)
    {
        while (true)
        {
            var maxChildIndex = this.GetMaxChild(parentIndex);

            if (this.IsValid(maxChildIndex)
                && this.IsGreater(maxChildIndex, parentIndex)) // child > parent
            {
                this.Swap(parentIndex, maxChildIndex);
                parentIndex = maxChildIndex;
            }
            else
            {
                break;
            }
        }
    }

    private void HeapifyDownRecursive(int parentIndex)
    {
        var maxChildIndex = this.GetMaxChild(parentIndex);

        if (this.IsValid(maxChildIndex)
            && this.IsGreater(maxChildIndex, parentIndex)) // child > parent
        {
            this.Swap(parentIndex, maxChildIndex);
            this.HeapifyDownRecursive(maxChildIndex);
        }
    }

    private bool IsGreater(int indexA, int indexB) // A > B
        => this.heap[indexA].CompareTo(this.heap[indexB]) > 0;

    private bool IsValid(int index) 
        => 0 <= index && index < this.Count;

    private int GetParentIndex(int childIndex) 
        => (childIndex - 1) / 2;

    private int GetLeftChildIndex(int parentIndex) 
        => parentIndex * 2 + 1;

    private int GetMaxChild(int parentIndex)
    {
        var leftChildIndex = this.GetLeftChildIndex(parentIndex);
        if (!this.IsValid(leftChildIndex))
        {
            return -1;
        }

        var rightChildIndex = leftChildIndex + 1;
        return this.IsValid(rightChildIndex)
            && this.IsGreater(rightChildIndex, leftChildIndex) // right > left child
            ? rightChildIndex
            : leftChildIndex;
    }

    private void Swap(int indexA, int indexB)
    {
        var temp = this.heap[indexA];
        this.heap[indexA] = this.heap[indexB];
        this.heap[indexB] = temp;
    }
}
