using System;
using System.Collections.Generic;
using System.Linq;

public class Tree<T>
{
    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this.Children = new List<Tree<T>>(children);
    }

    public T Value { get; }

    public IList<Tree<T>> Children { get; }

    public void Print(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + this.Value);

        foreach (var child in this.Children)
        {
            child.Print(indent + 2);
        }
    }

    public void Each(Action<T> action)
    {
        action(this.Value);

        foreach (var child in this.Children)
        {
            child.Each(action);
        }
    }

    public IEnumerable<T> OrderDFS()
    {
        // Recursion
        var result = new List<T>();
        this.RecursiveDFS(this, result);
        return result;

        //// Iteration
        //return this.IterativeDFS();
    }

    private void RecursiveDFS(Tree<T> current, IList<T> result)
    {
        foreach (var child in current.Children)
        {
            this.RecursiveDFS(child, result);
        }

        result.Add(current.Value);
    }

    private IEnumerable<T> IterativeDFS()
    {
        var result = new Stack<T>();

        var stack = new Stack<Tree<T>>();
        stack.Push(this);

        while (stack.Any())
        {
            var current = stack.Pop();
            result.Push(current.Value);

            foreach (var child in current.Children)
            {
                stack.Push(child);
            }
        }

        return result.ToArray();
    }

    public IEnumerable<T> OrderBFS()
    {
        var result = new List<T>();

        var queue = new Queue<Tree<T>>();
        queue.Enqueue(this);

        while (queue.Any())
        {
            var current = queue.Dequeue();
            result.Add(current.Value);

            foreach (var child in current.Children)
            {
                queue.Enqueue(child);
            }
        }

        return result;
    }
}
