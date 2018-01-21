using System;

public class ArrayStack<T>
{
    private const int DefaultCapacity = 16;

    private T[] collection;

    public ArrayStack(int capacity = DefaultCapacity)
    {
        this.collection = new T[capacity];
    }

    public int Count { get; private set; }

    private int Capacity => this.collection.Length;

    public void Push(T element)
    {
        if (this.Capacity == this.Count)
        {
            this.Grow();
        }

        this.collection[this.Count++] = element;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return this.collection[--this.Count];
    }

    public T[] ToArray()
    {
        var array = new T[this.Count];
        for (int i = 0; i < this.Count; i++)
        {
            array[i] = this.collection[this.Count - 1 - i];
        }

        return array;
    }

    private void Grow()
    {
        var newCollection = new T[this.Capacity * 2];
        Array.Copy(this.collection, newCollection, this.Count);

        this.collection = newCollection;
    }
}
