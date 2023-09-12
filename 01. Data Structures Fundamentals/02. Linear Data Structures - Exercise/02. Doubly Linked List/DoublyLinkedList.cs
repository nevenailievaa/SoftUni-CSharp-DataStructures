namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        //Node Class
        private class Node
        {
            //Constructor
            public Node(T value)
            {
                Value = value;
            }

            //Properties
            public T Value { get; set; }
            public Node Next { get; set; }
            public Node Previous { get; set; }
        }

        //Fields
        private Node head;
        private Node tail;

        //Constructor

        //Properties
        public int Count { get; private set; }

        //Methods
        public void AddFirst(T item)
        {
            Node newNode = new Node(item);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                head.Previous = newNode;
                newNode.Next = head;
                head = newNode;
            }
            
            Count++;
        }

        public void AddLast(T item)
        {
            Node newNode = new Node(item);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Previous = tail;
                tail = newNode;
            }

            Count++;
        }

        public T GetFirst()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The collection is empty.");
            }

            return head.Value;
        }

        public T GetLast()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The collection is empty.");
            }

            return tail.Value;
        }

        public T RemoveFirst()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The collection is empty.");
            }
            else if (Count == 1)
            {
                Node previousHead = head;
                head = null;
                Count--;
                return previousHead.Value;
            }
            else
            {
                Node previousHead = head;
                head = head.Next;
                head.Previous = null;
                Count--;
                return previousHead.Value;
            }
        }

        public T RemoveLast()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The collection is empty.");
            }
            else if (Count == 1)
            {
                Node previousTail = tail;
                tail = null;
                Count--;
                return previousTail.Value;
            }
            else
            {
                Node previousTail = tail;
                tail = tail.Previous;
                tail.Next = null;
                Count--;
                return previousTail.Value;
            }
        }

        //Enumerable Methods
        public IEnumerator<T> GetEnumerator()
        {
            Node currentNode = head;

            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}