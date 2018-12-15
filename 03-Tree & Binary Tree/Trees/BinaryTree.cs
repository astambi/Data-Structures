using System;

public class BinaryTree<T>
{
    public BinaryTree(T value,
        BinaryTree<T> leftChild = null,
        BinaryTree<T> rightChild = null)
    {
        this.Value = value;
        this.LeftChild = leftChild;
        this.RightChild = rightChild;
    }

    public T Value { get; }

    public BinaryTree<T> LeftChild { get; }

    public BinaryTree<T> RightChild { get; }

    // Pre-order: Root, Left, Right
    public void PrintIndentedPreOrder(int indent = 0)
    {
        Console.WriteLine($"{new string(' ', indent)}{this.Value}");

        if (this.LeftChild != null)
        {
            this.LeftChild.PrintIndentedPreOrder(indent + 2);
        }

        if (this.RightChild != null)
        {
            this.RightChild.PrintIndentedPreOrder(indent + 2);
        }
    }

    // In-order: Left, Root, Right
    public void EachInOrder(Action<T> action)
    {
        if (this.LeftChild != null)
        {
            this.LeftChild.EachInOrder(action);
        }

        action(this.Value);

        if (this.RightChild != null)
        {
            this.RightChild.EachInOrder(action);
        }
    }

    // Post-order: Left, Right, Root
    public void EachPostOrder(Action<T> action)
    {
        if (this.LeftChild != null)
        {
            this.LeftChild.EachPostOrder(action);
        }

        if (this.RightChild != null)
        {
            this.RightChild.EachPostOrder(action);
        }

        action(this.Value);
    }
}
