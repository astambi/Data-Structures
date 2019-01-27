using System;
using System.Collections.Generic;

public class IntervalTree
{
    private Node root;

    // O(log n)
    public void Insert(double lo, double hi)
        => this.root = this.Insert(this.root, lo, hi);

    public void EachInOrder(Action<Interval> action)
        => this.EachInOrder(this.root, action);

    // O(log n)
    public Interval SearchAny(double lo, double hi)
    {
        var current = this.root;

        while (current != null
            && !current.interval.Intersects(lo, hi))
        {
            if (current.left != null
                && current.left.max > lo)
            {
                current = current.left;
            }
            else
            {
                current = current.right;
            }
        }

        return current?.interval;
    }

    // O(log n)
    public IEnumerable<Interval> SearchAll(double lo, double hi)
    {
        var result = new List<Interval>();

        this.SearchAll(this.root, lo, hi, result);

        return result;
    }

    // Search In Order
    private void SearchAll(Node node, double lo, double hi, List<Interval> result)
    {
        if (node == null)
        {
            return;
        }

        // Search Left
        if (node.left != null
            && node.left.max > lo)
        {
            this.SearchAll(node.left, lo, hi, result);
        }

        // Search Root
        if (node.interval.Intersects(lo, hi))
        {
            result.Add(node.interval);
        }

        // Search Right
        if (node.right != null
            && node.right.interval.Lo < hi)
        {
            this.SearchAll(node.right, lo, hi, result);
        }
    }

    private void EachInOrder(Node node, Action<Interval> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.left, action);
        action(node.interval);
        this.EachInOrder(node.right, action);
    }

    // O(log n)
    private Node Insert(Node node, double lo, double hi)
    {
        if (node == null)
        {
            return new Node(new Interval(lo, hi));
        }

        int cmp = lo.CompareTo(node.interval.Lo);
        if (cmp < 0)
        {
            node.left = this.Insert(node.left, lo, hi);
        }
        else if (cmp > 0)
        {
            node.right = this.Insert(node.right, lo, hi);
        }

        // Update max endpoint
        this.UpdateMaxEndpoint(node);

        return node;
    }

    private void UpdateMaxEndpoint(Node node)
    {
        if (node == null)
        {
            return;
        }

        var maxChild = this.GetMaxNode(node.left, node.right);
        if (maxChild == null)
        {
            return;
        }

        var maxNode = this.GetMaxNode(node, maxChild);
        node.max = maxNode.max;
    }

    private Node GetMaxNode(Node firstNode, Node secondNode)
    {
        if (firstNode == null)
        {
            return secondNode;
        }

        if (secondNode == null)
        {
            return firstNode;
        }

        return firstNode.max > secondNode.max
            ? firstNode
            : secondNode;
    }

    private class Node
    {
        internal Interval interval;
        internal double max;
        internal Node right;
        internal Node left;

        public Node(Interval interval)
        {
            this.interval = interval;
            this.max = interval.Hi;
        }
    }
}
