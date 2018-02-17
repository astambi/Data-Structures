using System;
using System.Collections.Generic;

public class Trie<Value>
{
    private Node root;

    private class Node
    {
        public Value val;

        public bool isTerminal;

        public Dictionary<char, Node> next = new Dictionary<char, Node>();
    }

    public Value GetValue(string key)
    {
        var node = this.GetNode(this.root, key, 0);
        if (node == null || !node.isTerminal)
        {
            throw new InvalidOperationException();
        }

        return node.val;
    }

    public bool Contains(string key)
    {
        var node = this.GetNode(this.root, key, 0);
        return node != null && node.isTerminal;
    }

    public void Insert(string key, Value val)
    {
        this.root = this.Insert(this.root, key, val, 0);
    }

    public IEnumerable<string> GetByPrefix(string prefix)
    {
        var results = new Queue<string>();
        var node = this.GetNode(this.root, prefix, 0);

        this.Collect(node, prefix, results);

        return results;
    }

    private Node GetNode(Node node, string key, int index)
    {
        if (node == null)
        {
            return null;
        }

        if (index == key.Length)
        {
            return node;
        }

        var currentChar = key[index];
        var nextNode = NextNode(node, currentChar);

        return this.GetNode(nextNode, key, index + 1);
    }

    private Node NextNode(Node node, char currentChar)
    {
        Node nextNode = null;

        if (node.next.ContainsKey(currentChar))
        {
            nextNode = node.next[currentChar];
        }

        return nextNode;
    }

    private Node Insert(Node node, string key, Value val, int index)
    {
        if (node == null)
        {
            node = new Node();
        }

        if (index == key.Length)
        {
            node.isTerminal = true;
            node.val = val;
            return node;
        }

        var currentChar = key[index];
        var nextNode = NextNode(node, currentChar);

        node.next[currentChar] = this.Insert(nextNode, key, val, index + 1);

        return node;
    }

    private void Collect(Node node, string prefix, Queue<string> results)
    {
        if (node == null)
        {
            return;
        }

        if (node.val != null && node.isTerminal)
        {
            results.Enqueue(prefix);
        }

        foreach (var c in node.next.Keys)
        {
            this.Collect(node.next[c], prefix + c, results);
        }
    }
}