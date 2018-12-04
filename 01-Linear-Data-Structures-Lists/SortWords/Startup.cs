using System;
using System.Linq;

public class Startup
{
    public static void Main()
    {
        var words = Console.ReadLine()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .OrderBy(w => w)
            .ToList();

        Console.WriteLine(string.Join(" ", words));
    }
}

