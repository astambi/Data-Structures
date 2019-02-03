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

    public void Delete(T item)
        => this.root = this.Delete(this.root, item);

    public void DeleteMin()
    {
        var min = this.GetMinNode();
        if (min == null)
        {
            return;
        }

        this.root = this.Delete(this.root, min.Value);
    }

    public void EachInOrder(Action<T> action)
        => this.EachInOrder(this.root, action);

    public void Insert(T item)
        => this.root = this.Insert(this.root, item);

    private Node<T> Balance(Node<T> node)
    {
        var balance = this.Height(node.Left) - this.Height(node.Right);
        if (balance > 1)
        {
            var childBalance = this.Height(node.Left.Left) - this.Height(node.Left.Right);
            if (childBalance < 0)
            {
                node.Left = this.RotateLeft(node.Left);
            }

            node = this.RotateRight(node);
        }
        else if (balance < -1)
        {
            var childBalance = this.Height(node.Right.Left) - this.Height(node.Right.Right);
            if (childBalance > 0)
            {
                node.Right = this.RotateRight(node.Right);
            }

            node = this.RotateLeft(node);
        }

        return node;
    }

    private Node<T> Delete(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        var compare = item.CompareTo(node.Value);
        if (compare < 0)
        {
            node.Left = this.Delete(node.Left, item);
        }
        else if (compare > 0)
        {
            node.Right = this.Delete(node.Right, item);
        }
        else
        {
            // One child only => promote child
            if (node.Left == null)
            {
                return node.Right;
            }

            if (node.Right == null)
            {
                return node.Left;
            }

            // Two children => promote min Right (leftmost) & update Right
            var leftmost = this.FindLeftMostNode(node.Right); // min Right
            node.Value = leftmost.Value;
            node.Right = this.Delete(node.Right, leftmost.Value); // delete min Right & attach Right
        }

        // Rebalance
        node = this.Balance(node);

        // Update height
        this.UpdateHeight(node);

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

    private Node<T> FindLeftMostNode(Node<T> node)
    {
        if (node == null)
        {
            return null;
        }

        var leftmost = node;
        while (leftmost != null
            && leftmost.Left != null)
        {
            leftmost = leftmost.Left;
        }

        return leftmost;
    }

    private Node<T> GetMinNode()
        => this.FindLeftMostNode(this.root);

    private int Height(Node<T> node)
        => node == null
        ? 0
        : node.Height;

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

        // Rebalance
        node = this.Balance(node);

        // Update height
        this.UpdateHeight(node);

        return node;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        var right = node.Right;
        node.Right = right.Left;
        right.Left = node;

        this.UpdateHeight(node);

        return right;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        var left = node.Left;
        node.Left = left.Right;
        left.Right = node;

        this.UpdateHeight(node);

        return left;
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

    private void UpdateHeight(Node<T> node)
        => node.Height = Math.Max(this.Height(node.Left), this.Height(node.Right)) + 1;
}
