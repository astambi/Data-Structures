using System;

public class CircularQueue<T>
{
    private const int DefaultCapacity = 16;

    private T[] elements;
    private int startIndex;
    private int endIndex;

    public CircularQueue(int capacity = DefaultCapacity)
    {
        this.elements = new T[capacity];
    }

    public int Count { get; private set; }

    // O(1) / O(n)
    public void Enqueue(T element)
    {
        if (this.Count >= this.elements.Length)
        {
            this.Resize(); // O(n)
        }

        // O(1)
        this.elements[this.endIndex] = element;
        this.endIndex = (this.endIndex + 1) % this.elements.Length;
        this.Count++;
    }

    // O(1)
    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("Empty queue");
        }

        var removedElement = this.elements[this.startIndex];
        this.startIndex = (this.startIndex + 1) % this.elements.Length;
        this.Count--;

        return removedElement;
    }

    public T[] ToArray()
    {
        var values = new T[this.Count];
        this.CopyAllElements(values);

        return values;
    }

    private void CopyAllElements(T[] newArray)
    {
        for (int i = 0; i < newArray.Length; i++)
        {
            newArray[i] = this.elements[(this.startIndex + i) % this.elements.Length];
        }
    }

    private void Resize()
    {
        var newElements = new T[this.elements.Length * 2];

        this.CopyAllElements(newElements);

        this.elements = newElements;
        this.startIndex = 0;
        this.endIndex = this.Count;
    }
}

public class Example
{
    public static void Main()
    {
        CircularQueue<int> queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        int first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
