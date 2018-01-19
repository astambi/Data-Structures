namespace LinkedStack
{
    using System;

    public class Startup
    {
        public static void Main()
        {
            var stack = new LinkedStack<int>();
            stack.Push(100);
            stack.Push(200);
            stack.Push(300);

            var array = stack.ToArray();

            Console.WriteLine(string.Join(", ", stack.ToArray()));

            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(stack.Peek());

            Console.WriteLine(stack.Pop());
            Console.WriteLine(string.Join(", ", stack.ToArray()));
            Console.WriteLine(stack.Pop());

            Console.WriteLine(stack.Count);
            Console.WriteLine(string.Join(", ", stack.ToArray()));
        }
    }
}
