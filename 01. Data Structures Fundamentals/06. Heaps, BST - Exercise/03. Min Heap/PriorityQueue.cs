using System;
using System.Collections.Generic;

namespace _03.MinHeap
{
    public class PriorityQueue<T> : MinHeap<T> where T : IComparable<T>
    {
        //Constructor
        public PriorityQueue()
        {
            elements = new List<T>();
            indices = new Dictionary<T, int>();
        }

        //Methods
        public void Enqueue(T element) => Add(element);

        public T Dequeue() => ExtractMin();

        public void DecreaseKey(T key)
        {
            HeapifyUp(indices[key]);
        }
    }
}
