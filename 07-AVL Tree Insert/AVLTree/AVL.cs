using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root => this.root;

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    public void Insert(T item)
        => this.root = this.Insert(this.root, item);

    public void EachInOrder(Action<T> action)
        => this.EachInOrder(this.root, action);

    // O(ln N)
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

        // Rebalance tree
        node = this.RebalanceTree(node);

        // Update height
        this.UpdateHeight(node);

        return node;
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
            return this.Search(node.Left, item);
        }
        else if (cmp > 0)
        {
            return this.Search(node.Right, item);
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

    private int Height(Node<T> node)
        => node == null
        ? 0
        : node.Height;

    private int Balance(Node<T> node)
        => node == null
        ? 0
        : this.Height(node.Left) - this.Height(node.Right);

    private void UpdateHeight(Node<T> node)
    {
        if (node == null)
        {
            return;
        }

        node.Height = 1 + Math.Max(this.Height(node.Left), this.Height(node.Right));
    }

    private Node<T> RebalanceTree(Node<T> node)
    {
        var balance = this.Balance(node);

        // Right node heavier
        if (balance < -1)
        {
            balance *= this.Balance(node.Right);
            if (balance < 0) // double rotation required => rotate child first
            {
                node.Right = this.RotateRight(node.Right);
            }

            return this.RotateLeft(node);
        }

        // Left node heavier
        if (balance > 1)
        {
            balance *= this.Balance(node.Left);
            if (balance < 0) // double rotation required => rotate child first
            {
                node.Left = this.RotateLeft(node.Left);
            }

            return this.RotateRight(node);
        }

        // Balanced => [-1;1]
        return node;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        // Switch parent with left child preserving order
        var left = node.Left;
        node.Left = left.Right;
        left.Right = node;

        // Update height
        this.UpdateHeight(node);

        return left;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        // Switch parent with right child preserving order
        var right = node.Right;
        node.Right = right.Left;
        right.Left = node;

        // Update height
        this.UpdateHeight(node);

        return right;
    }
}
