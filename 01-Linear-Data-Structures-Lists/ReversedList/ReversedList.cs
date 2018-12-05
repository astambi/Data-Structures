using System;
using System.Collections;
using System.Collections.Generic;

public class ReversedList<T> : IEnumerable<T>
{
    private const int InitialCapacity = 2;
    private const int ResizeMultiple = 2;
    private T[] collection;

    public ReversedList()
    {
        this.collection = new T[InitialCapacity];
    }

    public void Add(T item)
    {
        if (this.collection.Length == this.Count)
        {
            this.Resize(this.Count * ResizeMultiple);
        }

        this.collection[this.Count++] = item;
    }

    public int Capacity => this.collection.Length;

    public int Count { get; private set; }

    public T this[int index]
    {
        get
        {
            this.Validate(index);
            return this.collection[this.ReversedIndex(index)];
        }

        set
        {
            this.Validate(index);
            this.collection[this.ReversedIndex(index)] = value;
        }
    }

    public T RemoveAt(int index)
    {
        this.Validate(index);

        var removedItem = this[index];
        this[index] = default(T);
        this.ShiftLeft(this.ReversedIndex(index));
        this.Count--;

        return removedItem;
    }

    private void ShiftLeft(int index)
    {
        for (int i = index; i < this.Count - 1; i++)
        {
            this.collection[i] = this.collection[i + 1];
        }

        this.collection[this.Count - 1] = default(T);
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < this.Count; i++)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private int ReversedIndex(int index) => this.Count - 1 - index;

    private void Resize(int newCapacity)
    {
        var resizedCollection = new T[newCapacity];
        Array.Copy(this.collection, resizedCollection, this.Count);
        this.collection = resizedCollection;
    }

    private void Validate(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}
