namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        //Node Class
        private class Node
        {
            //Properties
            public T Element { get; set; }
            public Node Next { get; set; }

            //Constructor
            public Node(T element, Node next)
            {
                this.Element = element;
                this.Next = next;
            }

            public Node(T element)
            {
                this.Element = element;
                this.Next = null;
            }
        }

        //Fields
        private Node head;

        //Properties
        public int Count {get; private set;}

        //Methods
        public void AddFirst(T item)
        {
            var currentNode = new Node(item, head);
            head = currentNode;
            Count++;
        }

        public void AddLast(T item)
        {
            Node currentNode = new Node(item);

            if (head == null)
            {
                head = currentNode;
            }
            else
            {
                Node node = head;

                while (node.Next != null)
                {
                    node = node.Next;
                }

                node.Next = currentNode;
            }
            Count++;
        }

        public T GetFirst()
        {
            //If the list is empty
            if (Count == 0)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            //Returning the first element
            return head.Element;
        }

        public T GetLast()
        {
            //If the list is empty
            if (Count == 0)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            //Returning the last element
            Node node = head;

            while (node.Next != null)
            {
                node = node.Next;
            }

            return node.Element;
        }

        public T RemoveFirst()
        {
            //If the list is empty
            if (Count == 0)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            //Removing the first element
            Node previousHead = head;
            head = previousHead.Next;
            Count--;

            return previousHead.Element;
        }

        public T RemoveLast()
        {
            //Queue is empty
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }

            //Queue has only one element
            if (this.head.Next == null)
            {
                var element = this.head;
                this.head = null;
                this.Count--;

                return element.Element;
            }

            //Finding and removing the last element
            Node node = this.head;

            while (node.Next.Next != null)
            {
                node = node.Next;
            }

            Node removeNode = node.Next;
            node.Next = null;

            this.Count--;
            return removeNode.Element;
        }

        //Enumerable Methods
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node node = head;

            while (node != null)
            {
                yield return node.Element;
                node = node.Next;
            }
        }
    }
}