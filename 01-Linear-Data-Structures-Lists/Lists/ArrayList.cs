using System;

public class ArrayList<T>
{
    private const int InitialCapacity = 2;
    private const int ResizeMultiple = 2;
    private const int DownsizeTrigger = 4;

    private T[] collection;

    public ArrayList(int capacity = InitialCapacity)
    {
        this.collection = new T[capacity];
        this.Capacity = capacity;
    }

    private int Capacity { get; set; }

    public int Count { get; private set; }

    public T this[int index]
    {
        get
        {
            this.ValidateIndex(index);
            return this.collection[index];
        }

        set
        {
            this.ValidateIndex(index);
            this.collection[index] = value;
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
        var removedElement = this[index];
        this[index] = default(T);

        this.ShiftLeft(index);
        this.Count--;

        if (this.Count <= this.Capacity / DownsizeTrigger)
        {
            this.Downsize();
        }

        return removedElement;
    }

    private void Downsize()
    {
        this.Capacity /= ResizeMultiple;

        var downsizedCollection = new T[this.Capacity];
        Array.Copy(this.collection, downsizedCollection, this.Count);
        this.collection = downsizedCollection;
    }

    private void Resize()
    {
        this.Capacity *= ResizeMultiple;

        var resizedCollection = new T[this.Capacity];
        Array.Copy(this.collection, resizedCollection, this.Count);
        this.collection = resizedCollection;
    }

    private void ShiftLeft(int index)
    {
        for (int i = index; i < this.Count; i++)
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
