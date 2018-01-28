using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }
    }

    private Node root;

    public BinarySearchTree()
    {
    }

    private BinarySearchTree(Node node)
    {
        this.Copy(node);
    }   

    public void Insert(T element)
    {
        if (this.root == null)
        {
            this.root = new Node(element);
            return;
        }

        Node parent = null;
        var current = this.root;

        while (current != null)
        {
            parent = current;

            if (current.Value.CompareTo(element) < 0) // current < element
            {
                current = current.Right;
            }
            else if (current.Value.CompareTo(element) > 0) // element < current
            {
                current = current.Left;
            }
            else
            {
                return;
            }
        }

        current = new Node(element);
        if (parent.Value.CompareTo(element) < 0) // parent < element
        {
            parent.Right = current;
        }
        else if (parent.Value.CompareTo(element) > 0) // element < parrent
        {
            parent.Left = current;
        }
    }

    public bool Contains(T element)
        => this.FindElement(element) != null;

    public void DeleteMin()
    {
        if (this.root == null)
        {
            return;
        }

        Node parent = null;
        var current = this.root;

        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }

        if (parent == null)
        {
            this.root = current.Right;
        }
        else
        {
            parent.Left = current.Right;
        }
    }

    public BinarySearchTree<T> Search(T element)
    {
        var current = this.FindElement(element);

        if (current == null)
        {
            return null;
        }

        return new BinarySearchTree<T>(current);
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        var queue = new Queue<T>();

        this.Range(this.root, queue, startRange, endRange);

        return queue;
    }

    public void EachInOrder(Action<T> action)
        => this.EachInOrder(this.root, action);

    private void Copy(Node node)
    {
        if (node == null)
        {
            return;
        }

        //  Pre-Order
        this.Insert(node.Value);
        this.Copy(node.Left);
        this.Copy(node.Right);
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if (node != null)
        {
            this.EachInOrder(node.Left, action);

            action(node.Value);

            this.EachInOrder(node.Right, action);
        }
    }

    private Node FindElement(T element)
    {
        var current = this.root;
        while (current != null)
        {
            if (current.Value.CompareTo(element) < 0) // current < element
            {
                current = current.Right;
            }
            else if (current.Value.CompareTo(element) > 0) // element < current 
            {
                current = current.Left;
            }
            else
            {
                break;
            }
        }

        return current;
    }

    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }

        var nodeInLowerRange = startRange.CompareTo(node.Value);
        var nodeInHigherRange = endRange.CompareTo(node.Value);

        if (nodeInLowerRange < 0) // start < node
        {
            this.Range(node.Left, queue, startRange, endRange);
        }

        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0) // start <= node <= end
        {
            queue.Enqueue(node.Value);
        }

        if (nodeInHigherRange > 0) // node < end
        {
            this.Range(node.Right, queue, startRange, endRange);
        }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        var tree = new BinarySearchTree<int>();
        tree.Insert(17);
        tree.Insert(9);
        tree.Insert(19);
        tree.Insert(6);
        tree.Insert(12);
        tree.Insert(25);
        tree.Insert(25);

        tree.EachInOrder(Console.WriteLine);
    }
}
