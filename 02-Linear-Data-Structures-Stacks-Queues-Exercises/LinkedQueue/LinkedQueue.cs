using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedQueue<T> : IEnumerable<T>
{
    private Node head;
    private Node tail;

    public int Count { get; private set; }

    public void Enqueue(T element)
    {
        var node = new Node(element);

        var oldTail = this.tail;
        this.tail = node;

        if (this.Count == 0)
        {
            this.head = node;
        }
        else
        {
            this.tail.Previous = oldTail;
            oldTail.Next = this.tail;
        }

        this.Count++;
    }

    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var removedElement = this.head.Value;
        this.Count--;

        if (this.Count == 0)
        {
            this.head = null;
            this.tail = null;
        }
        else
        {
            this.head = this.head.Next;
            this.head.Previous = null;
        }

        return removedElement;
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

    public T[] ToArray()
    {
        var array = new T[this.Count];

        var currentNode = this.head;
        for (int i = 0; i < this.Count; i++)
        {
            array[i] = currentNode.Value;
            currentNode = currentNode.Next;
        }

        return array;
    }

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public Node Next { get; set; }

        public Node Previous { get; set; }
    }
}
