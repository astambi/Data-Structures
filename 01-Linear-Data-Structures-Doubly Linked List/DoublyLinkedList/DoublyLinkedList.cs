using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    private ListNode<T> head;
    private ListNode<T> tail;

    public int Count { get; private set; }

    public void AddFirst(T element)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new ListNode<T>(element);
        }
        else
        {
            var newHead = new ListNode<T>(element);
            newHead.NextNode = this.head;

            this.head.PrevNode = newHead;
            this.head = newHead;
        }

        this.Count++;
    }

    public void AddLast(T element)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new ListNode<T>(element);
        }
        else
        {
            var newTail = new ListNode<T>(element);
            newTail.PrevNode = this.tail;

            this.tail.NextNode = newTail;
            this.tail = newTail;
        }

        this.Count++;
    }

    public T RemoveFirst()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("List empty");
        }

        var removedElement = this.head.Value;
        this.Count--;

        this.head = this.head.NextNode;
        if (this.head != null)
        {
            this.head.PrevNode = null;
        }
        else
        {
            this.tail = null;
        }

        return removedElement;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("List empty");
        }

        var removedElement = this.tail.Value;
        this.Count--;

        this.tail = this.tail.PrevNode;
        if (this.tail != null)
        {
            this.tail.NextNode = null;
        }
        else
        {
            this.head = null;
        }

        return removedElement;
    }

    public void ForEach(Action<T> action)
    {
        var currentNode = this.head;
        while (currentNode != null)
        {
            action(currentNode.Value);
            currentNode = currentNode.NextNode;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var currentNode = this.head;
        while (currentNode != null)
        {
            yield return currentNode.Value;
            currentNode = currentNode.NextNode;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    public T[] ToArray()
    {
        var values = new T[this.Count];
        var currentNode = this.head;
        var index = 0;

        while (currentNode != null)
        {
            values[index++] = currentNode.Value;
            currentNode = currentNode.NextNode;
        }

        return values;
    }

    private class ListNode<T>
    {
        public ListNode(T value)
        {
            this.Value = value;
        }

        public T Value { get; }

        public ListNode<T> NextNode { get; set; }

        public ListNode<T> PrevNode { get; set; }
    }
}

class Example
{
    static void Main()
    {
        var list = new DoublyLinkedList<int>();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.AddLast(5);
        list.AddFirst(3);
        list.AddFirst(2);
        list.AddLast(10);
        Console.WriteLine("Count = {0}", list.Count);

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        Console.WriteLine(string.Join(" ", list.ToArray()));
        Console.WriteLine("--------------------");

        list.RemoveFirst();
        list.RemoveLast();
        list.RemoveFirst();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveLast();
        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");
    }
}
