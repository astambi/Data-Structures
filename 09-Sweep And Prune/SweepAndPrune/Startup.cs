using System;
using System.Collections.Generic;
using System.Linq;

public class Startup
{
    public static void Main()
    {
        var rectangles = ReadGameRectangles();

        Sort(rectangles); // Sort by X1

        ProcessCommands(rectangles);
    }

    private static List<Rectangle> ReadGameRectangles()
    {
        var rectangles = new List<Rectangle>();

        while (true)
        {
            var line = Console.ReadLine();
            if (line == "start")
            {
                break;
            }

            var args = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (args.Length < 4 || args[0] != "add")
            {
                continue;
            }

            var name = args[1];
            var x1 = int.Parse(args[2]);
            var y1 = int.Parse(args[3]);

            rectangles.Add(new Rectangle(name, x1, y1));
        }

        return rectangles;
    }

    private static void ProcessCommands(List<Rectangle> rectangles)
    {
        var moves = 0;

        while (true)
        {
            var line = Console.ReadLine();
            if (line == "end")
            {
                break;
            }

            var args = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (args.Length < 1)
            {
                continue;
            }

            // Move rectangle
            var command = args[0];
            if (command == "move" && args.Length >= 4)
            {
                var name = args[1];
                var newX1 = int.Parse(args[2]);
                var newY1 = int.Parse(args[3]);

                var rectangle = rectangles.FirstOrDefault(r => r.Name == name);

                if (rectangle != null)
                {
                    rectangle.Update(newX1, newY1);
                    Sort(rectangles); // Sort by X1
                }
            }

            // Try Detect Collisions
            DetectCollisions(rectangles, ++moves);
        }
    }

    private static void DetectCollisions(List<Rectangle> rectangles, int moves)
    {
        var count = rectangles.Count;

        for (int leftIndex = 0; leftIndex < count - 1; leftIndex++)
        {
            var leftRectangle = rectangles[leftIndex];
            for (int rightIndex = leftIndex + 1; rightIndex < count; rightIndex++)
            {
                var rightRectangle = rectangles[rightIndex];

                // Collisions to the right not possible
                if (leftRectangle.IsLeftOf(rightRectangle))
                {
                    break;
                }

                // Collision detected
                if (leftRectangle.Intersects(rightRectangle))
                {
                    Console.WriteLine($"({moves}) {leftRectangle.Name} collides with {rightRectangle.Name}");
                }
            }
        }
    }

    private static void Sort(List<Rectangle> rectangles)
        => rectangles.Sort((a, b) => a.X1.CompareTo(b.X1));
}
