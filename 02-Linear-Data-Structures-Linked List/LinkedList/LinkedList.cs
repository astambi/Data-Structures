using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    private Node<T> head;
    private Node<T> tail;

    public int Count { get; private set; }

    // O(1)
    public void AddFirst(T item)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new Node<T>(item);
        }
        else
        {
            var newHead = new Node<T>(item);
            newHead.Next = this.head;

            this.head = newHead;
        }

        this.Count++;
    }

    // O(1)
    public void AddLast(T item)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new Node<T>(item);
        }
        else
        {
            var newTail = new Node<T>(item);
            this.tail.Next = newTail;

            this.tail = newTail;
        }

        this.Count++;
    }

    // O(1)
    public T RemoveFirst()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("List empty");
        }

        var removedItem = this.head.Value;
        this.Count--;

        if (this.Count == 0)
        {
            this.head = this.tail = null;
        }
        else
        {
            this.head = this.head.Next;
        }

        return removedItem;
    }

    // O(n)
    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("List empty");
        }

        var removedItem = this.tail.Value;
        this.Count--;

        if (this.Count == 0)
        {
            this.head = this.tail = null;
        }
        else
        {
            var tailPrev = this.FindTailPreviousNode();
            tailPrev.Next = null;

            this.tail = tailPrev;
        }

        return removedItem;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var currentNode = this.head;
        while (currentNode != null)
        {
            yield return currentNode.Value;
            currentNode = currentNode.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private Node<T> FindTailPreviousNode()
    {
        var tailPrev = this.head;
        for (int i = 0; i < this.Count - 1; i++)
        {
            tailPrev = tailPrev.Next;
        }

        return tailPrev;
    }

    private class Node<T>
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; }

        public Node<T> Next { get; set; }
    }
}
