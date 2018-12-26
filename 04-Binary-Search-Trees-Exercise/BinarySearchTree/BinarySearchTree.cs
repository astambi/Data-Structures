using System;
using System.Collections.Generic;

public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable
{
    private const string TreeEmptyMsg = "Tree empty";
    private const string NotFoundMsg = "Not found";

    private Node root;

    private Node FindElement(T element)
    {
        Node current = this.root;
        while (current != null)
        {
            var compare = current.Value.CompareTo(element);

            if (compare > 0)
            {
                current = current.Left;
            }
            else if (compare < 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        return current;
    }

    private void PreOrderCopy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.PreOrderCopy(node.Left);
        this.PreOrderCopy(node.Right);
    }

    private Node Insert(T element, Node node)
    {
        if (node == null)
        {
            node = new Node(element);
        }
        else if (element.CompareTo(node.Value) < 0)
        {
            node.Left = this.Insert(element, node.Left);
        }
        else if (element.CompareTo(node.Value) > 0)
        {
            node.Right = this.Insert(element, node.Right);
        }

        node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

        return node;
    }

    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }

        int nodeInLowerRange = startRange.CompareTo(node.Value);
        int nodeInHigherRange = endRange.CompareTo(node.Value);

        if (nodeInLowerRange < 0)
        {
            this.Range(node.Left, queue, startRange, endRange);
        }

        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
        {
            queue.Enqueue(node.Value);
        }

        if (nodeInHigherRange > 0)
        {
            this.Range(node.Right, queue, startRange, endRange);
        }
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

    private BinarySearchTree(Node node)
    {
        this.PreOrderCopy(node);
    }

    public BinarySearchTree()
    { }

    public void Insert(T element) 
        => this.root = this.Insert(element, this.root);

    public bool Contains(T element) 
        => this.FindElement(element) != null;

    public void EachInOrder(Action<T> action) 
        => this.EachInOrder(this.root, action);

    public BinarySearchTree<T> Search(T element) 
        => new BinarySearchTree<T>(this.FindElement(element));

    public void DeleteMin()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException(TreeEmptyMsg);
        }

        this.root = this.DeleteMin(this.root);
    }

    private Node DeleteMin(Node node)
    {
        if (node.Left == null)
        {
            return node.Right;
        }

        node.Left = this.DeleteMin(node.Left);
        node.Count--;

        return node;
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> queue = new Queue<T>();
        this.Range(this.root, queue, startRange, endRange);
        return queue;
    }

    public void Delete(T element)
    {
        if (this.root == null || !this.Contains(element))
        {
            throw new InvalidOperationException(NotFoundMsg);
        }

        this.root = this.Delete(element, this.root);
    }

    private Node Delete(T element, Node node)
    {
        if (node == null)
        {
            return null;
        }

        var compare = element.CompareTo(node.Value);
        if (compare < 0) // element < node
        {
            node.Left = this.Delete(element, node.Left);
        }
        else if (compare > 0)
        {
            node.Right = this.Delete(element, node.Right);
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
            node.Right = this.Delete(leftmost.Value, node.Right); // delete min Right & attach Right
        }

        node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

        return node;
    }

    private Node FindLeftMostNode(Node node)
    {
        var current = node;
        while (current.Left != null)
        {
            current = current.Left;
        }

        return current;
    }

    public void DeleteMax()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException(TreeEmptyMsg);
        }

        this.root = this.DeleteMax(this.root);
    }

    private Node DeleteMax(Node node)
    {
        if (node.Right == null)
        {
            return node.Left;
        }

        node.Right = this.DeleteMax(node.Right);
        node.Count--;

        return node;
    }

    public int Count() 
        => this.Count(this.root);

    private int Count(Node node) 
        => node == null ? 0 : node.Count;

    public int Rank(T element) 
        => this.Rank(element, this.root);

    private int Rank(T element, Node node) // count values smaller than element
    {
        if (node == null)
        {
            return 0;
        }

        var compare = element.CompareTo(node.Value);
        if (compare < 0) // element < node => go left
        {
            return this.Rank(element, node.Left);
        }

        if (compare > 0) // node < element
        {
            return this.Count(node.Left) + 1 + this.Rank(element, node.Right); // count left elems & go right
        }

        return this.Count(node.Left); // smaller than node
    }

    public T Select(int rank)
    {
        var node = this.Select(rank, this.root);
        if (node == null)
        {
            throw new InvalidOperationException(NotFoundMsg);
        }

        return node.Value;
    }

    private Node Select(int rank, Node node) // element having count of smaller elems < rank
    {
        if (node == null)
        {
            return null;
        }

        var leftCount = this.Count(node.Left);
        if (rank < leftCount)
        {
            return this.Select(rank, node.Left);
        }

        if (rank > leftCount)
        {
            return this.Select(rank - leftCount - 1, node.Right);
        }

        return node;
    }

    public T Ceiling(T element)
    {
        if (this.root == null)
        {
            throw new InvalidOperationException(TreeEmptyMsg);
        }

        return this.Select(this.Rank(element) + 1);
    }

    public T Floor(T element)
    {
        if (this.root == null)
        {
            throw new InvalidOperationException(TreeEmptyMsg);
        }

        return this.Select(this.Rank(element) - 1);
    }

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public int Count { get; set; }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        // Insert
        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(7);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        bst.EachInOrder(Console.WriteLine);
        Console.WriteLine($"Count: {bst.Count()}");

        // Rank
        Console.WriteLine($"Rank of 0: {bst.Rank(0)}");
        Console.WriteLine($"Rank of 1: {bst.Rank(1)}");
        Console.WriteLine($"Rank of 2: {bst.Rank(2)}");
        Console.WriteLine($"Rank of 3: {bst.Rank(3)}");
        Console.WriteLine($"Rank of 4: {bst.Rank(4)}");
        Console.WriteLine($"Rank of 5: {bst.Rank(5)}");
        Console.WriteLine($"Rank of 45: {bst.Rank(45)}");
        Console.WriteLine($"Rank of 46: {bst.Rank(46)}");

        // Select
        Console.WriteLine($"Select 0: {bst.Select(0)}");
        Console.WriteLine($"Select 4: {bst.Select(4)}");
        Console.WriteLine($"Select 9: {bst.Select(9)}");

        // Floor & Ceiling
        Console.WriteLine($"Floor 5: {bst.Floor(5)}");
        Console.WriteLine($"Ceiling 4: {bst.Ceiling(4)}");

        // Delete 
        Console.WriteLine("Delete");
        bst.Delete(5);
        bst.Delete(37);
        bst.EachInOrder(Console.WriteLine);
        Console.WriteLine($"Count: {bst.Count()}");

        // Delete Min/Max
        Console.WriteLine("Delete Min");
        bst.DeleteMin();
        bst.EachInOrder(Console.WriteLine);
        Console.WriteLine($"Count: {bst.Count()}");

        Console.WriteLine("Delete Max");
        bst.DeleteMax();
        bst.EachInOrder(Console.WriteLine);
        Console.WriteLine($"Count: {bst.Count()}");

        try
        {
            //Console.WriteLine($"Select 10: {bst.Select(10)}");
            //Console.WriteLine($"Floor 0: {bst.Floor(0)}");
            //Console.WriteLine($"Ceiling 45: {bst.Ceiling(45)}");
            bst.Delete(0);

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Delete Max");
                bst.DeleteMax();
                bst.EachInOrder(Console.WriteLine);
                Console.WriteLine($"Count: {bst.Count()}");

                Console.WriteLine("Delete Min");
                bst.DeleteMin();
                bst.EachInOrder(Console.WriteLine);
                Console.WriteLine($"Count: {bst.Count()}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
