using System;
using System.Collections.Generic;
using System.Linq;

public class Startup
{
    static Dictionary<int, Tree<int>> nodes = new Dictionary<int, Tree<int>>();

    public static void Main()
    {
        ReadTree();

        //PrintRootNode();
        //PrintTree();
        //PrintLeafNodes();
        //PrintMiddleNodes();
        //PrintDeepestNode();
        //PrintLongestPath();
        //PrintPathsWithGivenSum();
        PrintSubtreesWithGivenSum();
    }

    private static void PrintSubtreesWithGivenSum()
    {
        var targetSum = int.Parse(Console.ReadLine());
        var roots = new List<Tree<int>>();
        GetSubtreesOfSum(GetRootNode(), roots, targetSum, 0);

        Console.WriteLine($"Subtrees of sum {targetSum}:");
        foreach (var root in roots)
        {
            var nodes = new List<int>();
            GetSubtreeNodesPreOrder(root, nodes);

            Console.WriteLine(string.Join(" ", nodes));
        }
    }

    private static void GetSubtreeNodesPreOrder(Tree<int> node, List<int> nodesPreOrder)
    {
        nodesPreOrder.Add(node.Value);

        foreach (var child in node.Children)
        {
            GetSubtreeNodesPreOrder(child, nodesPreOrder);
        }
    }

    private static int GetSubtreesOfSum(Tree<int> node, List<Tree<int>> roots, int targetSum, int currentSum)
    {
        if (node == null)
        {
            return 0;
        }

        currentSum = node.Value;

        foreach (var child in node.Children)
        {
            currentSum += GetSubtreesOfSum(child, roots, targetSum, currentSum);
        }

        if (targetSum == currentSum)
        {
            roots.Add(node);
        }

        return currentSum;
    }

    private static void PrintPathsWithGivenSum()
    {
        var targetSum = int.Parse(Console.ReadLine());

        var leaves = new List<Tree<int>>();
        GetLeavesOfSum(GetRootNode(), leaves, targetSum, 0);

        Console.WriteLine($"Paths of sum {targetSum}:");
        foreach (var leaf in leaves)
        {
            Console.WriteLine(string.Join(" ", GetPath(leaf)));
        }
    }

    private static void GetLeavesOfSum(Tree<int> node, List<Tree<int>> result, int targetSum, int currentSum)
    {
        if (node == null)
        {
            return;
        }

        currentSum += node.Value;
        if (targetSum == currentSum && !node.Children.Any())
        {
            result.Add(node);
        }

        foreach (var child in node.Children)
        {
            GetLeavesOfSum(child, result, targetSum, currentSum);
        }
    }

    private static void PrintLongestPath()
    {
        var deepestNode = GetDeepestNodeDFS();
        var path = GetPath(deepestNode);

        Console.WriteLine($"Longest path: {string.Join(" ", path)}");
    }

    private static Stack<int> GetPath(Tree<int> node)
    {
        var path = new Stack<int>();
        while (node != null)
        {
            path.Push(node.Value);
            node = node.Parent;
        }

        return path;
    }

    private static void PrintDeepestNode() // Leftmost
    {
        //var deepestNode = GetDeepestNodeBFS();
        var deepestNode = GetDeepestNodeDFS();

        Console.WriteLine($"Deepest node: {deepestNode.Value}");
    }

    private static Tree<int> GetDeepestNodeDFS()
    {
        Tree<int> deepestNode = null;
        int maxDepth = 0;
        GetDeepestNodeDFS(GetRootNode(), ref maxDepth, 0, ref deepestNode);

        return deepestNode;
    }

    private static void GetDeepestNodeDFS(Tree<int> node, ref int maxDepth, int currentDepth, ref Tree<int> deepestNode)
    {
        if (node == null)
        {
            return;
        }

        if (currentDepth > maxDepth)
        {
            maxDepth = currentDepth;
            deepestNode = node;
        }

        foreach (var child in node.Children)
        {
            GetDeepestNodeDFS(child, ref maxDepth, currentDepth + 1, ref deepestNode);
        }
    }

    private static Tree<int> GetDeepestNodeBFS()
    {
        var queue = new Queue<Tree<int>>();
        queue.Enqueue(GetRootNode());

        Tree<int> current = null;
        while (queue.Count > 0)
        {
            current = queue.Dequeue();
            for (int i = current.Children.Count - 1; i >= 0; i--)
            {
                queue.Enqueue(current.Children[i]);
            }
        }

        return current;
    }

    private static void PrintMiddleNodes()
    {
        var middleNodes = nodes
            .Values
            .Where(n => n.Parent != null
                     && n.Children.Any())
            .Select(n => n.Value)
            .OrderBy(n => n)
            .ToList();

        Console.WriteLine($"Middle nodes: {string.Join(" ", middleNodes)}");
    }

    private static void PrintLeafNodes()
    {
        var leaves = nodes
            .Values
            .Where(n => !n.Children.Any())
            .Select(n => n.Value)
            .OrderBy(n => n)
            .ToList();

        Console.WriteLine($"Leaf nodes: {string.Join(" ", leaves)}");
    }

    private static void PrintTree()
        => GetRootNode().Print();

    private static void PrintRootNode()
    {
        var root = GetRootNode();
        if (root != null)
        {
            Console.WriteLine($"Root node: {root.Value}");
        }
    }

    private static Tree<int> GetRootNode()
        => nodes
            .Values
            .Where(n => n.Parent == null)
            .FirstOrDefault();

    private static void ReadTree()
    {
        var nodesCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < nodesCount - 1; i++)
        {
            var values = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            AddNode(values[0], values[1]);
        }
    }

    private static void AddNode(int parentValue, int childValue)
    {
        var parentNode = GetNode(parentValue);
        var childNode = GetNode(childValue);

        parentNode.Children.Add(childNode);
        childNode.Parent = parentNode;
    }

    private static Tree<int> GetNode(int value)
    {
        if (!nodes.ContainsKey(value))
        {
            nodes[value] = new Tree<int>(value);
        }

        return nodes[value];
    }
}
