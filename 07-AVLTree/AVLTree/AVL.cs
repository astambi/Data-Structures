using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root
        => this.root;

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    public void Insert(T item)
        => this.root = this.Insert(this.root, item);

    public void EachInOrder(Action<T> action)
        => this.EachInOrder(this.root, action);

    private Node<T> Insert(Node<T> node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item);
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = this.Insert(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = this.Insert(node.Right, item);
        }

        // Balance tree
        node = BalanceTree(node);

        return node;
    }

    private Node<T> BalanceTree(Node<T> node)
    {
        // Update Height
        this.UpdateHeight(node);

        // Balance Tree
        var balance = this.Height(node.Left) - this.Height(node.Right);

        if (balance > 1) // Left branch is heavier => Right Rotation
        {
            var leftChildBalance = this.Height(node.Left.Left) - this.Height(node.Left.Right);

            if (leftChildBalance < 0) // Double Rotation (Left - Right)
            {
                node.Left = this.RotateLeft(node.Left);
                node = this.RotateRight(node);
            }
            else // Single Rotation 
            {
                node = this.RotateRight(node);
            }
        }
        else if (balance < -1) // Right branch is heavier => Left Rotation
        {
            var rightChildBalance = this.Height(node.Right.Left) - this.Height(node.Right.Right);

            if (rightChildBalance > 0) // Double Rotation (Right - Left)
            {
                node.Right = this.RotateRight(node.Right);
                node = this.RotateLeft(node);
            }
            else // Single Rotation
            {
                node = this.RotateLeft(node);
            }
        }

        // Update Height
        this.UpdateHeight(node); // NB!

        return node;
    }

    private int Height(Node<T> node)
        => node == null ? 0 : node.Height;

    private void UpdateHeight(Node<T> node)
        => node.Height = 1 + Math.Max(this.Height(node.Left), this.Height(node.Right));

    private Node<T> RotateLeft(Node<T> node)
    {
        var newNode = node.Right;
        node.Right = node.Right.Left;
        newNode.Left = node;

        this.UpdateHeight(node);

        return newNode;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        var newNode = node.Left;
        node.Left = node.Left.Right;
        newNode.Right = node;

        this.UpdateHeight(node);

        return newNode;
    }

    private Node<T> Search(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            return Search(node.Left, item);
        }
        else if (cmp > 0)
        {
            return Search(node.Right, item);
        }

        return node;
    }

    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }
}
