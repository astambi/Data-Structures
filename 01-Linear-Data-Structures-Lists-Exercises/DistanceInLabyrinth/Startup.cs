namespace DistanceInLabyrinth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Startup
    {
        private const char StartingPoint = '*';
        private const char FullCell = 'x';
        private const string UnreachableCell = "u";

        public static void Main()
        {
            var size = int.Parse(Console.ReadLine());
            var labyrinth = ReadLabyrinth(size);
            var distances = CalculateDistances(size, labyrinth);
            Print(distances);
        }

        private static string[][] CalculateDistances(int size, char[][] labyrinth)
        {
            var distances = SetInitialPositions(labyrinth);
            var startingCell = FindStartingCell(labyrinth);
            if (startingCell == null)
            {
                return distances;
            }

            var cellsToUpdate = new Queue<Cell>();
            startingCell.Distance = 1;
            cellsToUpdate.Enqueue(startingCell);

            while (cellsToUpdate.Count > 0)
            {
                var current = cellsToUpdate.Dequeue();
                
                // Update distance to surrounding cells
                UpdateDistanceToCell(cellsToUpdate, distances, new Cell(current.Row - 1, current.Col, current.Distance));
                UpdateDistanceToCell(cellsToUpdate, distances, new Cell(current.Row + 1, current.Col, current.Distance));
                UpdateDistanceToCell(cellsToUpdate, distances, new Cell(current.Row, current.Col - 1, current.Distance));
                UpdateDistanceToCell(cellsToUpdate, distances, new Cell(current.Row, current.Col + 1, current.Distance));
            }

            return distances;
        }

        private static void UpdateDistanceToCell(Queue<Cell> cellsToUpdate, string[][] distances, Cell cell)
        {
            if (IsValidCell(cell.Row, cell.Col, distances.Length)
                && distances[cell.Row][cell.Col] == UnreachableCell)
            {
                distances[cell.Row][cell.Col] = cell.Distance.ToString();
                cellsToUpdate.Enqueue(new Cell(cell.Row, cell.Col, cell.Distance + 1));
            }
        }

        private static bool IsValidCell(int row, int col, int size)
            => row >= 0 && row < size
            && col >= 0 && col < size;

        private static Cell FindStartingCell(char[][] labyrinth)
        {
            for (int row = 0; row < labyrinth.Length; row++)
            {
                for (int col = 0; col < labyrinth.Length; col++)
                {
                    if (labyrinth[row][col] == StartingPoint)
                    {
                        return new Cell(row, col);
                    }
                }
            }

            return null;
        }

        private static string[][] SetInitialPositions(char[][] labyrinth)
        {
            var distances = new string[labyrinth.Length][];
            for (int r = 0; r < distances.Length; r++)
            {
                distances[r] = labyrinth[r]
                    .Select(e =>
                            (e == StartingPoint || e == FullCell)
                            ? e.ToString()
                            : UnreachableCell)
                    .ToArray();
            }

            return distances;
        }

        private static char[][] ReadLabyrinth(int size)
        {
            var labyrinth = new char[size][];
            for (int row = 0; row < size; row++)
            {
                labyrinth[row] = Console.ReadLine().ToCharArray();
            }

            return labyrinth;
        }

        private static void Print(string[][] distances)
        {
            for (int row = 0; row < distances.Length; row++)
            {
                Console.WriteLine(string.Join(string.Empty, distances[row]));
            }
        }

        private class Cell
        {
            public Cell(int row, int col, int distance = 0)
            {
                this.Row = row;
                this.Col = col;
                this.Distance = distance;
            }

            public int Row { get; private set; }

            public int Col { get; private set; }

            public int Distance { get; set; }
        }
    }
}
