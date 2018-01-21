namespace Node
{
    using System;

    public class Startup
    {
        public static void Main()
        {
            var firstNode = new Node<int>(100);
            var secondNode = new Node<int>(200);
            var thirdNode = new Node<int>(300);

            firstNode.Next = secondNode;
            secondNode.Next = thirdNode;

            var current = firstNode;
            while (current != null)
            {
                Console.WriteLine(current.Value);
                current = current.Next;
            }
        }
    }
}
