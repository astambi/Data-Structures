using System;
using System.Linq;

public class Startup
{
    public static void Main()
    {
        var numbers = Console.ReadLine()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        if (!numbers.Any())
        {
            numbers.Add(default(int));
        }

        Console.WriteLine($"Sum={numbers.Sum()}; Average={numbers.Average():F2}");
    }
}
