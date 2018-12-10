using System;
using System.Collections.Generic;
using System.Linq;

public class Startup
{
    public static void Main()
    {
        var numbers = Console.ReadLine()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse);

        var stack = new Stack<int>(numbers);
        Console.WriteLine(string.Join(" ", stack));
    }
}
