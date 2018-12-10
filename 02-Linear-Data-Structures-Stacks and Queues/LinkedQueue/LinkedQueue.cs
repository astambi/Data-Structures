using System;

public class LinkedQueue<T>
{
    private QueueNode<T> head;
    private QueueNode<T> tail;

    public int Count { get; private set; }

    // O(1)
    public void Enqueue(T element)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new QueueNode<T>(element);
        }
        else
        {
            // AddFirst
            var newHead = new QueueNode<T>(element);
            newHead.NextNode = this.head;

            this.head.PrevNode = newHead;
            this.head = newHead;
        }

        this.Count++;
    }

    // O(1)
    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("Empty queue");
        }

        // Remove Last
        this.Count--;
        var removedElement = this.tail.Value;

        this.tail = this.tail.PrevNode;
        if (this.tail == null)
        {
            this.head = null;
        }
        else
        {
            this.tail.NextNode = null;
        }

        return removedElement;
    }

    public T[] ToArray()
    {
        var values = new T[this.Count];
        var current = this.head;
        var index = this.Count - 1;

        while (index >= 0)
        {
            values[index--] = current.Value;
            current = current.NextNode;
        }

        return values;
    }

    private class QueueNode<T>
    {
        public QueueNode(T value)
        {
            this.Value = value;
        }

        public T Value { get; }

        public QueueNode<T> NextNode { get; set; }

        public QueueNode<T> PrevNode { get; set; }
    }
}

