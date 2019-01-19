//namespace Hierarchy.Core
//{
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Hierarchy<T> : IHierarchy<T>
{
    private readonly Node root;
    private readonly Dictionary<T, Node> nodes = new Dictionary<T, Node>();

    public Hierarchy(T rootValue)
    {
        this.root = new Node(rootValue);
        this.nodes[rootValue] = this.root;
    }

    public int Count
        => this.nodes.Count;

    public void Add(T parentValue, T childValue)
    {
        if (!this.Contains(parentValue)
            || this.Contains(childValue))
        {
            throw new ArgumentException();
        }

        var parentNode = this.nodes[parentValue];
        var childNode = new Node(childValue, parentNode);
        parentNode.Children.Add(childNode);

        this.nodes[childValue] = childNode;
    }

    public void Remove(T element)
    {
        if (!this.Contains(element))
        {
            throw new ArgumentException();
        }

        var nodeToRemove = this.nodes[element];
        if (nodeToRemove.Parent == null) // root
        {
            throw new InvalidOperationException();
        }

        var parent = nodeToRemove.Parent;
        var children = nodeToRemove.Children;

        // Remove element
        parent.Children.Remove(nodeToRemove);
        this.nodes.Remove(element);

        // Transfer children to parent
        children.ForEach(ch => ch.Parent = parent);
        parent.Children.AddRange(children);
    }

    public IEnumerable<T> GetChildren(T parentValue)
    {
        if (!this.Contains(parentValue))
        {
            throw new ArgumentException();
        }

        return this.nodes[parentValue]
            .Children
            .Select(x => x.Value);
    }

    public T GetParent(T parentValue)
    {
        if (!this.Contains(parentValue))
        {
            throw new ArgumentException();
        }

        var parentNode = this.nodes[parentValue].Parent;

        return parentNode == null
            ? default(T)
            : parentNode.Value;
    }

    public bool Contains(T nodeValue)
        => this.nodes.ContainsKey(nodeValue);

    public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        => this.nodes.Keys
        .Where(x => other.Contains(x));

    public IEnumerator<T> GetEnumerator()
    {
        var queue = new Queue<Node>();
        queue.Enqueue(this.root);

        while (queue.Any())
        {
            var current = queue.Dequeue();
            yield return current.Value;

            current
                .Children
                .ForEach(ch => queue.Enqueue(ch));
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();

    private class Node
    {
        public Node(T value, Node parent = null)
        {
            this.Value = value;
            this.Parent = parent;
        }

        public T Value { get; set; }

        public Node Parent { get; set; }

        public List<Node> Children { get; set; } = new List<Node>();
    }
}
//}