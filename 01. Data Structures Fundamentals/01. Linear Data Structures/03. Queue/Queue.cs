namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        //Node Class
        private class Node
        {
            //Properties
            public T element { get; set; }
            public Node Next { get; set; }

            //Constructor
            public Node(T element)
            {
                this.element = element;
                this.Next = null;
            }
        }

        //Fields
        private Node head;

        //Properties
        public int Count { get; private set; }

        //Methods
        public void Enqueue(T item)
        {
            //Checking if the queue is empty
            if (head == null)
            {
                head = new Node(item);
                Count++;
                return;
            }

            //Finding the last node
            Node currentNode = head;
            while (currentNode.Next != null)
            {
                currentNode = currentNode.Next;
            }

            //Enqueueing the item
            currentNode.Next= new Node(item);
            Count++;
        }

        public T Dequeue()
        {
            //Checking if the queue is empty
            if (Count == 0)
            {
                throw new InvalidOperationException("The queue is already empty.");
            }

            //Dequeueing by changing the head
            Node previousHead = head;
            head = previousHead.Next;
            Count--;
            return previousHead.element;
        }

        public T Peek()
        {
            //If the queue is empty
            if (Count == 0)
            {
                throw new InvalidOperationException("There is nothing to peek, the queue is empty!");
            }
            return head.element;
        }

        public bool Contains(T item)
        {
            //If the queue is empty
            if (Count == 0)
            {
                return false;
            }

            Node currentNode = head;

            //Searching the item
            while (currentNode.Next != null || currentNode.element.Equals(item))
            {
                if (currentNode.element.Equals(item))
                {
                    return true;
                }
                currentNode = currentNode.Next;
            }

            return false;
        }

        //Enumerable Methods
        public IEnumerator<T> GetEnumerator()
        {
            Node currentNode = head;

            for (int i = 1; i <= Count; i++)
            {
                yield return currentNode.element;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}