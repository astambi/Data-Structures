using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private Node root;

    public BinarySearchTree()
    {
        this.root = null;
    }

    private BinarySearchTree(Node node) : this()
    {
        this.Copy(node);
    }

    // O(n)
    public void Insert(T value)
    {
        this.InsertIteration(value);

        //this.root = this.InsertRecursion(value, this.root);
    }

    private Node InsertRecursion(T value, Node node)
    {
        if (node == null)
        {
            return new Node(value);
        }

        var compare = value.CompareTo(node.Value);
        if (compare < 0)
        {
            node.Left = this.InsertRecursion(value, node.Left);
        }
        else if (compare > 0)
        {
            node.Right = this.InsertRecursion(value, node.Right);
        }

        return node;
    }

    private void InsertIteration(T value)
    {
        if (this.root == null)
        {
            this.root = new Node(value);
            return;
        }

        var parent = this.FindParent(value);
        AttachNodeToParent(value, parent);
    }

    private static void AttachNodeToParent(T value, Node parent)
    {
        var compare = value.CompareTo(parent.Value);
        if (compare == 0)
        {
            return;
        }

        var newNode = new Node(value);
        if (compare < 0)
        {
            parent.Left = newNode;
        }
        else if (compare > 0)
        {
            parent.Right = newNode;
        }
    }

    private Node FindParent(T value)
    {
        Node parent = null;
        var current = this.root;

        while (current != null)
        {
            var compare = value.CompareTo(current.Value);
            if (compare == 0)
            {
                break;
            }

            parent = current;
            if (compare < 0)
            {
                current = current.Left;
            }
            else if (compare > 0)
            {
                current = current.Right;
            }
        }

        return parent;
    }

    public bool Contains(T value)
    {
        var current = this.root;
        while (current != null)
        {
            var compare = value.CompareTo(current.Value);
            if (compare == 0)
            {
                return true;
            }

            if (compare < 0)
            {
                current = current.Left;
            }
            else if (compare > 0)
            {
                current = current.Right;
            }
        }

        return false;
    }

    // O(n)
    public void DeleteMin()
    {
        if (this.root == null)
        {
            return;
        }

        // Find Min Element & Min Parent
        Node parent = null;
        var min = this.root;

        while (min.Left != null)
        {
            parent = min;
            min = min.Left;
        }

        // Replace Min with Min.Right
        if (parent != null)
        {
            parent.Left = min.Right;
        }
        else // min == root
        {
            this.root = min.Right;
        }
    }

    // O(n)
    public BinarySearchTree<T> Search(T item)
    {
        var current = this.root;
        while (current != null)
        {
            var compare = item.CompareTo(current.Value);
            if (compare == 0)
            {
                return new BinarySearchTree<T>(current);
            }

            if (compare < 0)
            {
                current = current.Left;
            }
            else if (compare > 0)
            {
                current = current.Right;
            }
        }

        //return new BinarySearchTree<T>();
        return null;
    }

    private void Copy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.Copy(node.Left);
        this.Copy(node.Right);
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        var result = new HashSet<T>();
        this.Range(startRange, endRange, this.root, result);
        return result;
    }

    private void Range(T start, T end, Node node, HashSet<T> result)
    {
        if (node == null)
        {
            return;
        }

        var compareStart = node.Value.CompareTo(start);
        var compareEnd = node.Value.CompareTo(end);

        // InOrder traversal
        if (compareStart > 0) // start < node => traverse left
        {
            this.Range(start, end, node.Left, result);
        }

        if (compareStart >= 0 && compareEnd <= 0) // in range => add to result 
        {
            result.Add(node.Value);
        }

        if (compareEnd < 0) // node < end => traverse right
        {
            this.Range(start, end, node.Right, result);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; private set; }

        public Node Left { get; set; }

        public Node Right { get; set; }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        var tree = new BinarySearchTree<int>();
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(1);
        tree.Insert(1);
        tree.Insert(5);
        tree.Insert(4);

        Console.Write("EachInOrder: ");
        tree.EachInOrder(x => Console.Write(x + " "));
        Console.WriteLine();

        Console.WriteLine($"Contains 2: {tree.Contains(2)}");
        Console.WriteLine($"Contains 9: {tree.Contains(9)}");

        Console.Write("Search 9: ");
        var searchTree = tree.Search(9);
        if (searchTree == null)
        {
            searchTree = new BinarySearchTree<int>();
        }
        searchTree.EachInOrder(x => Console.Write(x + " "));
        Console.WriteLine();

        Console.Write("Insert 9: ");
        searchTree.Insert(9);
        searchTree.EachInOrder(x => Console.Write(x + " "));
        Console.WriteLine();
        Console.WriteLine($"Contains 9: {searchTree.Contains(9)}");

        Console.Write("Range [2; 4]: ");
        Console.WriteLine(string.Join(" ", tree.Range(2, 4)));

        for (int i = 0; i < 5; i++)
        {
            Console.Write("Delete Min: ");
            tree.DeleteMin();
            tree.EachInOrder(x => Console.Write(x + " "));
            Console.WriteLine();
        }
    }
}
