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

        numbers
            .Distinct()
            .Select(n => new
            {
                Number = n,
                Count = numbers.Count(e => e == n)
            })
            .OrderBy(n => n.Number)
            .ToList()
            .ForEach(n => Console.WriteLine($"{n.Number} -> {n.Count} times"));
    }
}
