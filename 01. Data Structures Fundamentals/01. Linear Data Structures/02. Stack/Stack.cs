namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class Stack<T> : IAbstractStack<T>
    {
        //Node Class
        private class Node
        {
            //Properties
            public T Element { get; set; }
            public Node Next { get; set; }

            //Constructors
            public Node(T element, Node next)
            {
                this.Element = element;
                this.Next = next;
            }

            public Node(T element)
            {
                this.Element = element;
            }
        }

        //Fields
        private Node top;

        //Properties
        public int Count {get; private set;}

        //Methods
        public void Push(T item)
        {
            Node node = new Node(item, this.top);
            this.top = node;
            Count++;
        }

        public T Pop()
        {
            //If the collection is empty
            if (Count == 0)
            {
                throw new InvalidOperationException("Stack is empty.");
            }

            //Changing the top element
            var oldTop = this.top;
            this.top = oldTop.Next;

            Count--;
            return oldTop.Element;
        }

        public T Peek()
        {
            //If the collection is empty
            if(Count == 0)
            {
                throw new InvalidOperationException("Stack is empty.");
            }
            return this.top.Element;
        }

        public bool Contains(T item)
        {
            //Empty Collection
            if (Count == 0)
            {
                return false;
            }

            //Iterating through the stack's elements
            Node currentNode = this.top;

            while (currentNode != null)
            {
                //Finding the searched item
                if (currentNode.Element.Equals(item))
                {
                    return true;
                }

                currentNode = currentNode.Next;
            }
            
            //Item not found
            return false;
        }

        //Enumerable Methods
        public IEnumerator<T> GetEnumerator()
        {
            var node = this.top;

            while (node != null)
            {
                yield return node.Element;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}