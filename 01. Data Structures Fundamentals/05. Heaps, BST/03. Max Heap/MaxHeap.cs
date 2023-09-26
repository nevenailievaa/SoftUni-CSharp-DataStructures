namespace _03.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T> where T : IComparable<T>
    {
        //Fields
        private List<T> elements;

        //Constructor
        public MaxHeap()
        {
            elements = new List<T>();
        }

        //Properties
        public int Size => elements.Count;

        //Methods
        public void Add(T element)
        {
            elements.Add(element);
            HeapifyUp(elements.Count - 1);
        }

        private void HeapifyUp(int index)
        {
            var parentIndex = GetParentIndex(index);

            while (index > 0 && IsGreater(index, parentIndex))
            {
                Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = GetParentIndex(index);
            }
        }

        private bool IsGreater(int index, int parentIndex) => elements[index].CompareTo(elements[parentIndex]) > 0;

        private void Swap(int index, int parentIndex)
        {
            var temp = elements[index];
            elements[index] = elements[parentIndex];
            elements[parentIndex] = temp;
        }

        private int GetParentIndex(int index) => (index - 1) / 2;

        public T ExtractMax()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException();
            }

            T element = elements[0];
            Swap(0, elements.Count - 1);
            elements.RemoveAt(Size - 1);
            HeapifyDown(0);

            return element;
        }
        private void HeapifyDown(int index)
        {
            var biggerChildIndex = GetBiggerChildIndex(index);

            while (IsIndexValid(biggerChildIndex) && IsGreater(biggerChildIndex, index))
            {
                Swap(biggerChildIndex, index);
                biggerChildIndex = GetBiggerChildIndex(index);
            }
        }

        private bool IsIndexValid(int index) => index >= 0 && index < Size;

        private int GetBiggerChildIndex(int index)
        {
            var firstChildIndex = index * 2 + 1;
            var secondChildIndex = index * 2 + 2;

            if (secondChildIndex < Size)
            {
                if (IsGreater(firstChildIndex, secondChildIndex))
                {
                    return firstChildIndex;
                }
                return secondChildIndex;
            }
            else if (firstChildIndex < Size)
            {
                return firstChildIndex;
            }
            else
            {
                return -1;
            }
        }

        public T Peek()
        {
            if (elements.Count == 0)
            {
                throw new InvalidOperationException();
            }
            return elements[0];
        }
    }
}