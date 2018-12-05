using System;
using System.Collections.Generic;
using System.Linq;

public class Startup
{
    private const char Start = '*';
    private const string Empty = "0";
    private const string Unreachable = "u";

    public static void Main()
    {
        var labyrinth = ReadLabyrinth();
        var startingPosition = GetStartingPosition(labyrinth);
        var distances = CalculateDistancesFromStart(labyrinth, startingPosition);
        Print(distances);
    }

    private static void Print(string[][] distances)
    {
        foreach (var row in distances)
        {
            Console.WriteLine(string.Join(
                string.Empty,
                row.Select(x => x != Empty ? x : Unreachable)));
        }
    }

    private static string[][] CalculateDistancesFromStart(char[][] labyrinth, Cell startingPosition)
    {
        var distances = InitializeDistances(labyrinth);

        if (startingPosition == null)
        {
            return distances;
        }

        // BFS 
        var queue = new Queue<Cell>();
        queue.Enqueue(startingPosition);

        while (queue.Any())
        {
            var current = queue.Dequeue();

            UpdateDistanceToCell(current.Row - 1, current.Col, current.Distance + 1, queue, distances);
            UpdateDistanceToCell(current.Row + 1, current.Col, current.Distance + 1, queue, distances);
            UpdateDistanceToCell(current.Row, current.Col - 1, current.Distance + 1, queue, distances);
            UpdateDistanceToCell(current.Row, current.Col + 1, current.Distance + 1, queue, distances);
        }

        return distances;
    }

    private static void UpdateDistanceToCell(int row, int col, int distance, Queue<Cell> queue, string[][] distances)
    {
        if (IsValidCell(row, col, distances.Length)
            && distances[row][col] == Empty)
        {
            queue.Enqueue(new Cell(row, col, distance));
            distances[row][col] = distance.ToString();
        }
    }

    private static string[][] InitializeDistances(char[][] labyrinth)
    {
        var distances = new string[labyrinth.Length][];
        for (int row = 0; row < labyrinth.Length; row++)
        {
            distances[row] = labyrinth[row]
                .Select(x => x.ToString())
                .ToArray();
        }

        return distances;
    }

    private static Cell GetStartingPosition(char[][] labyrinth)
    {
        for (int row = 0; row < labyrinth.Length; row++)
        {
            for (int col = 0; col < labyrinth[row].Length; col++)
            {
                if (labyrinth[row][col] == Start)
                {
                    return new Cell(row, col);
                }
            }
        }

        return null;
    }

    private static char[][] ReadLabyrinth()
    {
        var size = int.Parse(Console.ReadLine());
        var labyrinth = new char[size][];

        for (int row = 0; row < size; row++)
        {
            labyrinth[row] = Console.ReadLine().ToCharArray();
        }

        return labyrinth;
    }

    private static bool IsValidCell(int row, int col, int matrixSize)
        => row >= 0 && row < matrixSize
        && col >= 0 && col < matrixSize;

    private class Cell
    {
        public Cell(int row, int col, int distance = 0)
        {
            this.Row = row;
            this.Col = col;
            this.Distance = distance;
        }

        public int Row { get; }

        public int Col { get; }

        public int Distance { get; }
    }
}

