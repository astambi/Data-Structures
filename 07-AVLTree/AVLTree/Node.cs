using System;

public class Node<T> where T : IComparable<T>
{
    public Node(T value)
    {
        this.Value = value;
        this.Height = 1;
    }

    public T Value;

    public Node<T> Left;

    public Node<T> Right;

    public int Height;
}
