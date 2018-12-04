using System;

public class ArrayList<T>
{
    private const int InitialCapacity = 2;
    private const int IncreaseMultiple = 2;
    private const int DecreaseMultiple = 4;
    private T[] collection;

    public ArrayList(int capacity = InitialCapacity)
    {
        this.collection = new T[capacity];
    }

    public int Count { get; private set; }

    public T this[int index]
    {
        get
        {
            this.Validate(index);
            return this.collection[index];
        }

        set
        {
            this.Validate(index);
            this.collection[index] = value;
        }
    }

    public void Add(T item)
    {
        if (this.Count >= this.collection.Length)
        {
            this.ResizeCollection(this.collection.Length * IncreaseMultiple);
        }

        this.collection[this.Count++] = item;
    }

    public T RemoveAt(int index)
    {
        var removedElement = this[index]; // with validation
        this[index] = default(T);
        this.Count--;
        this.ShiftLeft(index);

        if (this.Count <= this.collection.Length / DecreaseMultiple)
        {
            this.ResizeCollection(this.collection.Length / DecreaseMultiple);
        }

        return removedElement;
    }

    private void Validate(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    private void ResizeCollection(int size)
    {
        var collectionCopy = new T[size];
        Array.Copy(this.collection, collectionCopy, this.Count);
        this.collection = collectionCopy;
    }

    private void ShiftLeft(int index)
    {
        for (int i = index; i < this.Count; i++)
        {
            this.collection[i] = this.collection[i + 1];
        }
    }
}
