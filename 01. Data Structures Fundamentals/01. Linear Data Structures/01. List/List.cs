namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class List<T> : IAbstractList<T>
    {
        //Fields
        private const int defaultCapacity = 4;
        private T[] items;

        //Constructors
        public List()
            : this(defaultCapacity) 
        {
            items = new T[defaultCapacity];
        }

        public List(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }
            items = new T[capacity];
        }

        //Properties
        public T this[int index] 
        {
            get
            {
                ValidateIndex(index);
                return items[index];
            }
            set
            {
                ValidateIndex(index);
                items[index] = value;
            }
        }

        public int Count { get; private set; }

        //Methods
        public void Add(T item)
        {
            if (Count == items.Length)
            {
                GrowCapacity();
            }

            items[Count] = item;
            Count++;
        }

        public bool Contains(T item)
        {
            return this.IndexOf(item) != -1 ? true : false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (items[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (Count == items.Length)
            {
                GrowCapacity();
            }

            ValidateIndex(index);

            for (int i = Count; i > index; i--)
            {
                items[i] = items[i-1];
            }

            items[index] = item;
            Count++;
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);

            if (index == -1)
            {
                return false;
            }

            for (int i = index; i < Count-1; i++)
            {
                items[i] = items[i+1];
            }

            items[Count-1] = default(T);
            Count--;

            return true;
        }

        public void RemoveAt(int index)
        {
            ValidateIndex(index);

            for (int i = index; i < Count-1; i++)
            {
                items[i] = items[i+1];
            }

            items[Count-1] = default(T);
            Count--;
        }

        //Private Methods
        private void GrowCapacity()
        {
            T[] itemsCopy = new T[items.Length * 2];
            Array.Copy(items, itemsCopy, Count);
            items = itemsCopy;
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("Invalid index given!");
            }
        }

        //Enumerable Methods
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}