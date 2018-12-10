using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedStack<T> : IEnumerable<T>
{
    private Node<T> top;

    public int Count { get; private set; }

    public void Push(T item)
    {
        this.top = new Node<T>(item, this.top);
        this.Count++;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("Empty list");
        }

        this.Count--;
        var removedElement = this.top.Value;
        this.top = this.top.NextNode;

        return removedElement;
    }

    public T Peek()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("Empty list");
        }

        return this.top.Value;
    }

    public T[] ToArray()
    {
        var values = new T[this.Count];
        var currentNode = this.top;
        var index = 0;

        while (currentNode != null)
        {
            values[index++] = currentNode.Value;
            currentNode = currentNode.NextNode;
        }

        return values;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var currentNode = this.top;
        while (currentNode != null)
        {
            yield return currentNode.Value;
            currentNode = currentNode.NextNode;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private class Node<T>
    {
        public Node(T value, Node<T> nextNode = null)
        {
            this.Value = value;
            this.NextNode = nextNode;
        }

        public T Value { get; }

        public Node<T> NextNode { get; }
    }
}

