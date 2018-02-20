using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{
    private LinkedList<T> listByInsertion;
    private OrderedBag<LinkedListNode<T>> listAsc;
    private OrderedBag<LinkedListNode<T>> listDesc;

    public FirstLastList()
    {
        this.listByInsertion = new LinkedList<T>();
        this.listAsc = new OrderedBag<LinkedListNode<T>>((x, y) => x.Value.CompareTo(y.Value));
        this.listDesc = new OrderedBag<LinkedListNode<T>>((x, y) => y.Value.CompareTo(x.Value));
    }

    public int Count
        => this.listByInsertion.Count;

    public void Add(T element)
    {
        var node = new LinkedListNode<T>(element);

        this.listByInsertion.AddLast(node);
        this.listAsc.Add(node);
        this.listDesc.Add(node);
    }

    public void Clear()
    {
        this.listByInsertion.Clear();
        this.listAsc.Clear();
        this.listDesc.Clear();
    }

    public IEnumerable<T> First(int count)
    {
        if (!IsValidCount(count))
        {
            throw new ArgumentOutOfRangeException();
        }

        var current = this.listByInsertion.First;
        int index = 0;

        while (index++ < count)
        {
            yield return current.Value;

            current = current.Next;
        }
    }

    public IEnumerable<T> Last(int count)
    {
        if (!IsValidCount(count))
        {
            throw new ArgumentOutOfRangeException();
        }

        var current = this.listByInsertion.Last;
        int index = 0;

        while (index++ < count)
        {
            yield return current.Value;

            current = current.Previous;
        }
    }

    public IEnumerable<T> Max(int count)
    {
        if (!IsValidCount(count))
        {
            throw new ArgumentOutOfRangeException();
        }

        return this.listDesc
           .Take(count)
           .Select(e => e.Value)
           .ToList();
    }

    public IEnumerable<T> Min(int count)
    {
        if (!IsValidCount(count))
        {
            throw new ArgumentOutOfRangeException();
        }

        return this.listAsc
            .Take(count)
            .Select(e => e.Value)
            .ToList();
    }

    public int RemoveAll(T element)
    {
        var elementNode = new LinkedListNode<T>(element);

        this.listAsc
            .Range(elementNode, true, elementNode, true)
            .ForEach(e => this.listByInsertion.Remove(e));

        this.listDesc.RemoveAllCopies(elementNode);

        return this.listAsc.RemoveAllCopies(elementNode);
    }

    private bool IsValidCount(int count)
        => 0 <= count && count <= this.Count;
}
