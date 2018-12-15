using System;
using System.Collections.Generic;
using System.Linq;

public class Startup
{
    private static IDictionary<int, Tree<int>> edges = new Dictionary<int, Tree<int>>();

    public static void Main()
    {
        ReadTree();

        Console.WriteLine($"Root node: {GetRootNode().Value}");
        PrintTree(GetRootNode());
        Console.WriteLine($"Leaf nodes: {string.Join(" ", GetLeafNodes())}");
        Console.WriteLine($"Middle nodes: {string.Join(" ", GetMiddleNodes())}");
        Console.WriteLine($"Deepest node: {GetDeepestNode().Value}");
        Console.WriteLine($"Longest path: {string.Join(" ", GetPathToRoot(GetDeepestNode()))}");

        var sum = int.Parse(Console.ReadLine());

        Console.WriteLine($"Paths of sum {sum}:");
        Console.WriteLine($"{string.Join(Environment.NewLine, GetPathsOfSum(sum))}");

        Console.WriteLine($"Subtrees of sum {sum}:");
        Console.WriteLine($"{string.Join(Environment.NewLine, GetSubtreesOfSum(sum))}");
    }

    private static IEnumerable<string> GetSubtreesOfSum(int sum)
        => edges
        .Values
        .Where(n => CalcSubtreeSum(n) == sum)
        .Select(n => string.Join(" ", GetSubtree(n)));

    private static IEnumerable<int> GetSubtree(Tree<int> node)
    {
        var result = new List<Tree<int>>();
        GetNodesPreOrder(node, result);

        return result.Select(n => n.Value).ToArray();
    }

    private static void GetNodesPreOrder(Tree<int> node, List<Tree<int>> result)
    {
        if (node == null)
        {
            return;
        }

        result.Add(node);
        foreach (var child in node.Children)
        {
            GetNodesPreOrder(child, result);
        }
    }

    private static int CalcSubtreeSum(Tree<int> node, int sum = 0)
    {
        if (node == null)
        {
            return 0;
        }

        sum += node.Value;
        foreach (var child in node.Children)
        {
            sum += CalcSubtreeSum(child);
        }

        return sum;
    }

    private static IEnumerable<string> GetPathsOfSum(int sum)
        => edges
        .Values
        .Where(n => !n.Children.Any()) // leaf nodes
        .Where(n => CalcPathToRootSum(n) == sum)
        .Select(n => string.Join(" ", GetPathToRoot(n)));

    private static int CalcPathToRootSum(Tree<int> node)
    {
        var sum = 0;
        var current = node;

        while (current != null)
        {
            sum += current.Value;
            current = current.Parent;
        }

        return sum;
    }

    private static IEnumerable<int> GetPathToRoot(Tree<int> node)
    {
        var path = new Stack<int>();
        var current = node;

        while (current != null)
        {
            path.Push(current.Value);
            current = current.Parent;
        }

        return path.ToArray();
    }

    private static Tree<int> GetDeepestNode() // leftmost
        => edges
        .Values
        .Where(n => !n.Children.Any()) // leaf nodes
        .Select(n => new
        {
            Node = n,
            Depth = CalcNodeDepth(n)
        })
        .OrderByDescending(n => n.Depth)
        .Select(n => n.Node)
        .FirstOrDefault(); // leftmost

    private static int CalcNodeDepth(Tree<int> node)
    {
        var depth = 0;
        var current = node;

        while (current != null)
        {
            depth++;
            current = current.Parent;
        }

        return depth;
    }

    private static IEnumerable<int> GetMiddleNodes()
        => edges
        .Values
        .Where(n => n.Parent != null)
        .Where(n => n.Children.Any())
        .Select(n => n.Value)
        .OrderBy(x => x); // ASC

    private static IEnumerable<int> GetLeafNodes()
        => edges
        .Values
        .Where(n => !n.Children.Any())
        .Select(n => n.Value)
        .OrderBy(x => x); // ASC

    private static void PrintTree(Tree<int> rootNode, int indent = 0)
    {
        if (rootNode == null)
        {
            return;
        }

        Console.WriteLine($"{new string(' ', indent)}{rootNode.Value}");

        foreach (var child in rootNode.Children)
        {
            PrintTree(child, indent + 2);
        }
    }

    private static Tree<int> GetRootNode()
        => edges
        .Values
        .FirstOrDefault(e => e.Parent == null);

    private static void ReadTree()
    {
        var nodesCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < nodesCount - 1; i++)
        {
            var values = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var parentValue = values[0];
            var childValue = values[1];

            AddEdge(parentValue, childValue);
        }
    }

    private static void AddEdge(int parentValue, int childValue)
    {
        var parentNode = GetNodeByValue(parentValue);
        var childNode = GetNodeByValue(childValue);

        parentNode.Children.Add(childNode);
        childNode.Parent = parentNode;
    }

    private static Tree<int> GetNodeByValue(int value)
    {
        if (!edges.ContainsKey(value))
        {
            edges[value] = new Tree<int>(value);
        }

        return edges[value];
    }
}
