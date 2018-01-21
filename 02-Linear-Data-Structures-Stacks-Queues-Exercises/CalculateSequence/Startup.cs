namespace CalculateSequence
{
    using System;
    using System.Collections.Generic;

    public class Startup
    {
        private const int SequenceElements = 50;

        public static void Main()
        {
            var firstElement = int.Parse(Console.ReadLine());
            var sequence = CalculateSequence(firstElement);
            Console.WriteLine(string.Join(", ", sequence));
        }

        private static int[] CalculateSequence(int firstElement)
        {
            var elements = new Queue<int>();
            elements.Enqueue(firstElement);

            int index = 0;
            var sequence = new int[SequenceElements];
            sequence[index++] = firstElement;

            while (index < SequenceElements)
            {
                var currentElement = elements.Dequeue();

                var nextElement1 = currentElement + 1;
                var nextElement2 = currentElement * 2 + 1;
                var nextElement3 = currentElement + 2;

                sequence[index++] = nextElement1;
                if (index >= SequenceElements)
                {
                    break;
                }

                sequence[index++] = nextElement2;
                if (index >= SequenceElements)
                {
                    break;
                }

                sequence[index++] = nextElement3;

                elements.Enqueue(nextElement1);
                elements.Enqueue(nextElement2);
                elements.Enqueue(nextElement3);
            }

            return sequence;
        }
    }
}
