using System;
using System.Collections.Generic;

public class AStar
{
    // Shortest path => min fCost = gCost + hCost
    // gCost = distance from current to start
    // hCost = distance from current to goal

    private const char Wall = 'W';

    private char[,] map;

    //private PriorityQueue<Node> priorityQueue;
    //private Dictionary<Node, Node> parents;
    //private Dictionary<Node, int> gCost;

    public AStar(char[,] map)
    {
        this.map = map;

        //this.priorityQueue = new PriorityQueue<Node>();
        //this.parents = new Dictionary<Node, Node>();
        //this.gCost = new Dictionary<Node, int>();
    }

    // hCost
    public static int GetH(Node current, Node goal)
    {
        // Manhattan distance calc
        var deltaX = Math.Abs(current.Col - goal.Col);
        var deltaY = Math.Abs(current.Row - goal.Row);

        return deltaX + deltaY;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        var priorityQueue = new PriorityQueue<Node>(); // min priority heap => min fCost
        var parents = new Dictionary<Node, Node>();
        var gCost = new Dictionary<Node, int>();

        priorityQueue.Enqueue(start);
        parents[start] = null;
        gCost[start] = 0;

        while (priorityQueue.Count > 0)
        {
            var current = priorityQueue.Dequeue();

            if (current.Equals(goal))
            {
                break;
            }

            var neighbours = this.AddAdjacentNodes(current);
            var newGCost = gCost[current] + 1;

            foreach (var neighbour in neighbours)
            {
                if (!gCost.ContainsKey(neighbour) || newGCost < gCost[neighbour])
                {
                    gCost[neighbour] = newGCost;
                    neighbour.F = newGCost + GetH(neighbour, goal); // fCost = gCost + hCost

                    parents[neighbour] = current;

                    priorityQueue.Enqueue(neighbour);
                }
            }
        }

        return this.ReconstructPath(parents, start, goal);
    }

    private IEnumerable<Node> ReconstructPath(Dictionary<Node, Node> parents, Node start, Node goal)
    {
        var path = new Stack<Node>();

        if (parents.ContainsKey(goal))
        {
            path.Push(goal);
            var current = parents[goal];

            while (current != start)
            {
                path.Push(current);
                current = parents[current];
            }
        }

        path.Push(start);
        return path;
    }

    private List<Node> AddAdjacentNodes(Node current)
    {
        var neighbours = new List<Node>();

        this.AddNeighbour(neighbours, current.Row - 1, current.Col);
        this.AddNeighbour(neighbours, current.Row + 1, current.Col);
        this.AddNeighbour(neighbours, current.Row, current.Col - 1);
        this.AddNeighbour(neighbours, current.Row, current.Col + 1);

        return neighbours;
    }

    private void AddNeighbour(List<Node> neighbours, int row, int col)
    {
        if (this.IsInsideMap(row, col) && this.IsAccessible(row, col))
        {
            var neighbour = new Node(row, col);
            neighbours.Add(neighbour);
        }
    }

    private bool IsInsideMap(int row, int col)
        => row >= 0 && row < this.map.GetLength(0)
        && col >= 0 && col < this.map.GetLength(1);

    private bool IsAccessible(int row, int col)
        => this.map[row, col] != Wall;
}
