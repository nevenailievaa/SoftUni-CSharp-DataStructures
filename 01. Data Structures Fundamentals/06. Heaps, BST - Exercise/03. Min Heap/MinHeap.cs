using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace _03.MinHeap
{
    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        //Fields
        protected List<T> elements;
        protected Dictionary<T, int> indices;

        //Constructor
        public MinHeap()
        {
            elements = new List<T>();
            indices = new Dictionary<T, int>();
        }

        //Properties
        public int Count => elements.Count;

        //Methods
        public void Add(T element)
        {
            elements.Add(element);
            indices.Add(element, elements.Count - 1);
            HeapifyUp(elements.Count - 1);
        }

        protected void HeapifyUp(int index)
        {
            var parentIndex = (index - 1) / 2;
            while (index > 0 && IsGreater(index, parentIndex))
            {
                Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
        }

        private void Swap(int first, int second)
        {
            (elements[first], elements[second]) = (elements[second], elements[first]);
            indices[elements[first]] = first;
            indices[elements[second]] = second;
        }

        private bool IsGreater(int index, int parentIndex)
        {
            return elements[index].CompareTo(elements[parentIndex]) < 0;
        }

        public T ExtractMin()
        {
            if(elements.Count == 0)
            {
                throw new InvalidOperationException();
            }
            T element = elements[0];

            Swap(0, elements.Count - 1);
            elements.RemoveAt(elements.Count - 1);
            indices.Remove(element);
            HeapifyDown(0);
            return element;
        }

        private void HeapifyDown(int index)
        {
            var smallerChildIndex = GetSmallerChildIndex(index);
            while (IsIndexValid(smallerChildIndex) && IsGreater(smallerChildIndex, index))
            {
                Swap(smallerChildIndex, index);
                index = smallerChildIndex;
                smallerChildIndex = GetSmallerChildIndex(index);
            }
        }

        private int GetSmallerChildIndex(int index)
        {
            var firstChildIndex = index * 2 + 1;
            var secondChildIndex = index * 2 + 2;

            if (secondChildIndex < elements.Count)
            {
                if (IsGreater(firstChildIndex, secondChildIndex))
                {
                    return firstChildIndex;
                }
                else
                {
                    return secondChildIndex;
                }
            }
            else if (firstChildIndex < elements.Count)
            {
                return firstChildIndex;
            }
            else
            {
                return -1;
            }
        }

        private bool IsIndexValid(int index)
        {
            return index >= 0 && index < elements.Count;
        }

        public T Peek()
        {
            if(elements.Count == 0)
            {
                throw new InvalidOperationException();
            }
            return elements[0];
        }
    }
}
