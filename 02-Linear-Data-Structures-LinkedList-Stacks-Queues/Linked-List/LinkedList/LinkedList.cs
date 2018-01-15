using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    private Node head;
    private Node tail; 

    public int Count { get; private set; }

    // O(1)
    public void AddFirst(T item)
    {
        var oldHead = this.head;

        this.head = new Node(item);
        this.head.Next = oldHead;

        if (this.IsEmpty)
        {
            this.tail = this.head;
        }

        this.Count++;
    }

    // O(1)
    public void AddLast(T item)
    {
        var oldTail = this.tail;
        this.tail = new Node(item);

        if (this.IsEmpty)
        {
            this.head = this.tail;
        }
        else
        {
            oldTail.Next = this.tail;
        }

        this.Count++;
    }

    // O(1)
    public T RemoveFirst()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var removedItem = this.head.Value;
        this.head = this.head.Next;
        this.Count--;

        if (this.IsEmpty)
        {
            this.tail = null;
        }

        return removedItem;
    }

    // O(n)
    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var removedItem = this.tail.Value;
        this.Count--;

        if (this.IsEmpty)
        {
            this.head = null;
            this.tail = null;
        }
        else
        {
            this.tail = this.GetSecondToLast();
        }

        return removedItem;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = this.head;

        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private bool IsEmpty => this.Count == 0;

    private Node GetSecondToLast()
    {
        var current = this.head;

        for (int i = 0; i < this.Count - 1; i++)
        {
            current = current.Next;
        }

        current.Next = null;
        return current;
    }

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public Node Next { get; set; }
    }
}
