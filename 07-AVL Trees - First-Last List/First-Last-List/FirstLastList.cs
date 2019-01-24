using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{
    private readonly LinkedList<T> linkedList = new LinkedList<T>(); // by insertion
    private readonly OrderedBag<LinkedListNode<T>> listAsc = new OrderedBag<LinkedListNode<T>>(
        (a, b) => a.Value.CompareTo(b.Value));
    private readonly OrderedBag<LinkedListNode<T>> listDesc = new OrderedBag<LinkedListNode<T>>(
        (a, b) => b.Value.CompareTo(a.Value));

    public int Count
        => this.linkedList.Count;

    public void Add(T element)
    {
        var node = new LinkedListNode<T>(element); //

        this.linkedList.AddLast(node);
        this.listAsc.Add(node);
        this.listDesc.Add(node);
    }

    public void Clear()
    {
        this.linkedList.Clear();
        this.listAsc.Clear();
        this.listDesc.Clear();
    }

    public IEnumerable<T> First(int count)
    {
        this.Validate(count);

        var current = this.linkedList.First;
        var countResult = 0;

        while (countResult < count)
        {
            yield return current.Value;
            current = current.Next;
            countResult++;
        }
    }

    public IEnumerable<T> Last(int count)
    {
        this.Validate(count);

        var current = this.linkedList.Last;
        var countResult = 0;

        while (countResult < count)
        {
            yield return current.Value;
            current = current.Previous;
            countResult++;
        }
    }

    public IEnumerable<T> Max(int count)
    {
        this.Validate(count);

        return this.listDesc
            .Take(count)
            .Select(n => n.Value);
    }

    public IEnumerable<T> Min(int count)
    {
        this.Validate(count);

        return this.listAsc
            .Take(count)
            .Select(n => n.Value);
    }

    public int RemoveAll(T element)
    {
        var node = new LinkedListNode<T>(element);

        this.listAsc
            .Range(node, true, node, true) // [node, node]
            .ForEach(n => this.linkedList.Remove(n));

        var countRemoved = this.listAsc.RemoveAllCopies(node);
        this.listDesc.RemoveAllCopies(node);

        return countRemoved;
    }

    private void Validate(int count)
    {
        if (!this.IsValid(count))
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    private bool IsValid(int count)
        => 0 <= count && count <= this.Count;
}
