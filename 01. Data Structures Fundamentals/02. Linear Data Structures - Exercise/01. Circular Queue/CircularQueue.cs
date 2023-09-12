namespace Problem01.CircularQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CircularQueue<T> : IAbstractQueue<T>
    {
        //Fields
        private T[] elements;
        private int startIndex;
        private int endIndex;

        //Constructor
        public CircularQueue(int capacity = 4)
        {
            elements = new T[capacity];
        }

        //Properties
        public int Count { get; private set; }

        //Methods
        public T Dequeue()
        {
            //If the queue is empty
            if (Count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            //Taking the first element, then emptying its index
            T firstElement = elements[startIndex];
            elements[startIndex] = default(T);

            //Checking if there is only one element, so the indexer shouldn't increase
            if (Count != 1)
            {
                startIndex++;
            }
            Count--;

            return firstElement;
        }

        public void Enqueue(T item)
        {
            //Checking if the queue is full, so it should grow more space
            if (Count >= elements.Length)
            {
                this.Grow();
            }

            //Enqueue logic
            elements[endIndex] = item;
            endIndex = (endIndex + 1) % elements.Length; 
            Count++;
        }

        public T Peek()
        {
            //If the queue is empty
            if (Count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            return elements[startIndex];
        }

        public T[] ToArray()
        {
            return CopyElements(new T[Count]);
        }

        //Private Methods
        private void Grow()
        {
            //First copying the elements
            elements = CopyElements(new T[Count * 2]);

            //Resetting the start and end indexes
            startIndex = 0;
            endIndex = Count;
        }

        private T[] CopyElements(T[] array)
        {
            //Copying the current array to a new one
            for (int i = 0; i < Count; i++)
            {
                array[i] = elements[(startIndex + i) % Count];
            }

            return array;
        }

        //Enumerable Methods
        public IEnumerator<T> GetEnumerator()
        {
            for (int currentIndex = 0; currentIndex < Count; currentIndex++)
            {
                yield return elements[(startIndex + currentIndex) % elements.Length];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}