namespace LinkedStack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class LinkedStack<T> : IEnumerable<T>
    {
        private StackNode top;

        public int Count { get; private set; }

        public void Push(T item)
        {
            this.top = new StackNode(item, this.top);
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var poppedItem = this.top.Value;
            this.top = this.top.Next;
            this.Count--;

            return poppedItem;
        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.top.Value;
        }

        public T[] ToArray()
        {
            if (this.Count == 0)
            {
                return null;
            }

            var array = new T[this.Count];
            var current = this.top;

            for (int i = 0; i < this.Count; i++)
            {
                array[i] = current.Value;
                current = current.Next;
            }

            return array;
        }

        public IEnumerator<T> GetEnumerator()
        {
            StackNode current = null;

            if (this.Count != 0)
            {
                current = this.top;
            }

            for (int i = 0; i < this.Count; i++)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private class StackNode
        {
            public StackNode(T value, StackNode nextNode)
            {
                this.Value = value;
                this.Next = nextNode;
            }

            public T Value { get; private set; }

            public StackNode Next { get; private set; }
        }
    }
}
