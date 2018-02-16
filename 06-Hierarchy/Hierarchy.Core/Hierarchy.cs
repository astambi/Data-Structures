using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class Hierarchy<T> : IHierarchy<T>
{
    private Node root;
    private Dictionary<T, Node> nodes;

    public Hierarchy(T root)
    {
        this.root = new Node(root);
        this.nodes = new Dictionary<T, Node>();
        this.nodes[root] = this.root;
    }

    public int Count
        => this.nodes.Count;

    public void Add(T element, T child)
    {
        if (!this.nodes.ContainsKey(element))
        {
            throw new ArgumentException();
        }

        if (this.nodes.ContainsKey(child))
        {
            throw new ArgumentException();
        }

        var parentNode = this.nodes[element];
        var childNode = new Node(child, parentNode);

        parentNode.Children.Add(childNode);
        this.nodes[child] = childNode;
    }

    public void Remove(T element)
    {
        if (!this.nodes.ContainsKey(element))
        {
            throw new ArgumentException();
        }

        var elementNode = this.nodes[element];
        if (elementNode.Equals(this.root))
        {
            throw new InvalidOperationException();
        }

        var parentNode = elementNode.Parent;
        var chilrenNodes = elementNode.Children;

        // Transfer element's children to parent
        foreach (var childNode in chilrenNodes)
        {
            childNode.Parent = parentNode;
            parentNode.Children.Add(childNode);
        }

        // Remove element
        parentNode.Children.Remove(elementNode);
        this.nodes.Remove(element);
    }

    public IEnumerable<T> GetChildren(T element)
    {
        if (!this.nodes.ContainsKey(element))
        {
            throw new ArgumentException();
        }

        return this.nodes[element]
            .Children
            .Select(c => c.Value)
            .ToList();
    }

    public T GetParent(T element)
    {
        if (!this.nodes.ContainsKey(element))
        {
            throw new ArgumentException();
        }

        var parentNode = this.nodes[element].Parent;

        return parentNode != null
            ? parentNode.Value
            : default(T);
    }

    public bool Contains(T value)
        => this.nodes.ContainsKey(value);

    public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        => this.nodes
        .Keys
        .Where(k => other.Contains(k))
        .ToList();

    public IEnumerator<T> GetEnumerator()
    {
        var elementsToTraverse = new Queue<Node>();
        elementsToTraverse.Enqueue(this.root);

        while (elementsToTraverse.Any())
        {
            var current = elementsToTraverse.Dequeue();
            yield return current.Value;

            foreach (var childNode in current.Children)
            {
                elementsToTraverse.Enqueue(childNode);
            }
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
            this.Children = new List<Node>();
        }

        public T Value { get; }

        public Node Parent { get; set; }

        public List<Node> Children { get; set; }
    }
}
