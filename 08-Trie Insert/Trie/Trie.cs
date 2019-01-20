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

        var currentSymbol = key[index];
        var nextNode = this.GetNextNode(node, currentSymbol);

        return this.GetNode(nextNode, key, index + 1);
    }

    private Node GetNextNode(Node node, char currentSymbol)
    {
        return node.next.ContainsKey(currentSymbol)
            ? node.next[currentSymbol]
            : null;
    }

    private Node Insert(Node node, string key, Value val, int index)
    {
        /* Check if the given node is null and create and assign a new node if it is
         * 
         * Check if you are at the last symbol of the key and if you are, do the following:
         * Make the node terminal
         * Assign the value
         * Return the node
         * 
         * Check if the symbol you're at is in the children of the node
         * Move to the next node if it is
         * 
         * Recursively call the insert for the next child of our node
         * 
         * Return the node
         */

        if (node == null)
        {
            node = new Node();
        }

        if (key.Length == index)
        {
            node.isTerminal = true;
            node.val = val;
            return node;
        }

        var currentSymbol = key[index];
        var nextNode = this.GetNextNode(node, currentSymbol);

        node.next[currentSymbol] = this.Insert(nextNode, key, val, index + 1);

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

        foreach (var key in node.next.Keys)
        {
            this.Collect(node.next[key], prefix + key, results);
        }
    }
}
