using System;

public class KdTree
{
    private const int K = 2; // 2D tree

    private Node root;

    public class Node
    {
        public Node(Point2D point)
        {
            this.Point = point;
        }

        public Point2D Point { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }
    }

    public Node Root
        => this.root;

    public bool Contains(Point2D point)
    {
        var current = this.root;
        var depth = 0;

        while (current != null)
        {
            if (point.CompareTo(current.Point) == 0)
            {
                return true;
            }

            int compare = this.CompareByDimension(current, point, depth);
            if (compare < 0)
            {
                current = current.Left;
            }
            else if (compare > 0)
            {
                current = current.Right;
            }

            depth++;
        }

        return false;
    }

    public void Insert(Point2D point)
        => this.root = this.Insert(this.root, point, 0);

    public void EachInOrder(Action<Point2D> action)
        => this.EachInOrder(this.root, action);

    private Node Insert(Node node, Point2D point, int depth)
    {
        if (node == null)
        {
            return new Node(point);
        }

        var compare = this.CompareByDimension(node, point, depth);
        if (compare < 0)
        {
            node.Left = this.Insert(node.Left, point, depth + 1);
        }
        else if (compare > 0)
        {
            node.Right = this.Insert(node.Right, point, depth + 1);
        }

        return node;
    }

    private void EachInOrder(Node node, Action<Point2D> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Point);
        this.EachInOrder(node.Right, action);
    }

    private int CompareByDimension(Node node, Point2D point, int depth)
    {
        var compare = depth % K;
        if (compare == 0)
        {
            compare = point.X.CompareTo(node.Point.X); // compare by 1st dimension
        }
        else
        {
            compare = point.Y.CompareTo(node.Point.Y); // compare by 2nd dimension
        }

        return compare;
    }
}
