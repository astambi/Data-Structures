using System;

public class LinkedStack<T>
{
    private Node top;

    public int Count { get; private set; }

    public void Push(T element)
    {
        this.top = new Node(element, this.top);
        this.Count++;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var removedElement = this.top.Value;
        this.top = this.top.NextNode;
        this.Count--;

        return removedElement;
    }

    public T[] ToArray()
    {
        var array = new T[this.Count];
        var currentNode = this.top;

        for (int i = 0; i < this.Count; i++)
        {
            array[i] = currentNode.Value;
            currentNode = currentNode.NextNode;
        }

        return array;
    }

    private class Node
    {
        public Node(T value, Node nextNode)
        {
            this.Value = value;
            this.NextNode = nextNode;
        }

        public T Value { get; private set; }

        public Node NextNode { get; private set; }
    }
}