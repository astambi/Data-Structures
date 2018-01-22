using System;
using System.Collections;
using System.Collections.Generic;

public class ReversedList<T> : IEnumerable<T>
{
    private const int DefaultCapacity = 2;

    private T[] collection;

    public ReversedList(int capacity = DefaultCapacity)
    {
        this.collection = new T[capacity];
    }

    public int Count { get; private set; }

    public int Capacity => this.collection.Length;

    private int ReversedIndex(int index) => this.Count - 1 - index;

    public T this[int index]
    {
        get
        {
            this.ValidateIndex(this.ReversedIndex(index));
            return this.collection[this.ReversedIndex(index)];
        }
    }

    public void Add(T item)
    {
        if (this.Count == this.Capacity)
        {
            this.Resize();
        }

        this.collection[this.Count++] = item;
    }

    public T RemoveAt(int index)
    {
        this.ValidateIndex(this.ReversedIndex(index));

        var removedElement = this.collection[this.ReversedIndex(index)];
        this.ShiftLeft(index);
        this.Count--;

        return removedElement;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < this.Count; i++)
        {
            yield return this.collection[this.ReversedIndex(i)];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private void Resize()
    {
        var newCollection = new T[this.Capacity * 2];
        Array.Copy(this.collection, newCollection, this.Count);

        this.collection = newCollection;
    }

    private void ShiftLeft(int index)
    {
        for (int i = this.ReversedIndex(index); i < this.Count; i++)
        {
            this.collection[i] = this.collection[i + 1];
        }
    }

    private void ValidateIndex(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}
