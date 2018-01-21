namespace ReverseNumbers
{
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

            var numbersStack = new Stack<int>(numbers);

            if (numbersStack.Count > 0)
            {
                Console.WriteLine(string.Join(" ", numbersStack));
            }
        }
    }
}
