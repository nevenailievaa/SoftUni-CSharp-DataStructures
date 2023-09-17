namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tree<T> : IAbstractTree<T>
    {
        //Fields
        private List<Tree<T>> children;
        private T value;
        private Tree<T> parent;

        //Constructors
        public Tree(T value)
        {
            this.value = value;
            children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children) : this(value)
        {
            foreach (var child in children)
            {
                child.parent = this;
                this.children.Add(child);
            }
        }

        //Methods
        public void AddChild(T parentKey, Tree<T> child)
        {
            var searchedParent = FindNodeWithBFS(parentKey);

            if (searchedParent == null)
            {
                throw new ArgumentNullException();
            }

            searchedParent.children.Add(child);
            child.parent = searchedParent;
        }

        private Tree<T> FindNodeWithBFS(T parentKey)
        {
            Queue<Tree<T>> queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                Tree<T> currentTree = queue.Dequeue();

                if (currentTree.value.Equals(parentKey))
                {
                    return currentTree;
                }

                foreach (var child in currentTree.children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        public IEnumerable<T> OrderBfs()
        {
            Queue<Tree<T>> queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            var result = new List<T>();

            while (queue.Count > 0)
            {
                Tree<T> currentTree = queue.Dequeue();
                result.Add(currentTree.value);

                foreach (var child in currentTree.children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public IEnumerable<T> OrderDfs()
        {
            List<T> list = new List<T>();
            this.OrderDfsWithRecursion(this, list);
            return list;
        }

        //Depth-First Search with recursion
        private void OrderDfsWithRecursion(Tree<T> currentTree, ICollection<T> result)
        {
            foreach (var child in currentTree.children)
            {
                child.OrderDfsWithRecursion(child, result);
            }
            result.Add(currentTree.value);
        }

        //Depth-First Search with Stack
        public IEnumerable<T> OrderDfsWithStack()
        {
            Stack<Tree<T>> stack = new Stack<Tree<T>>();
            stack.Push(this);

            Stack<T> result = new Stack<T>();

            while (stack.Count > 0)
            {
                var currentTree = stack.Pop();

                foreach (var child in currentTree.children)
                {
                    stack.Push(child);
                }

                result.Push(currentTree.value);
            }
            return result;
        }

        public void RemoveNode(T nodeKey)
        {
            var searchedNode = FindNodeWithBFS(nodeKey);

            //If we try to remove an unexisting node
            if (searchedNode is null)
            {
                throw new ArgumentNullException();
            }

            var parentNode = searchedNode.parent;

            //If we try to remove the root node
            if (parentNode is null)
            {
                throw new ArgumentException();
            }

            //Removing
            parentNode.children = parentNode.children.Where(c => !c.value.Equals(searchedNode.value)).ToList();
            searchedNode.parent = null;
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = FindNodeWithBFS(firstKey);
            var secondNode = FindNodeWithBFS(secondKey);

            //If at least one of the nodes doesn't exist
            if (firstNode == null || secondNode == null)
            {
                throw new ArgumentNullException();
            }

            var firstParent = firstNode.parent;
            var secondParent = secondNode.parent;

            //If trying to swap the root
            if (firstParent == null || secondParent == null)
            {
                throw new ArgumentException();
            }

            var indexOfFirstChild = firstParent.children.IndexOf(firstNode);
            var indexOfSecondChild = secondParent.children.IndexOf(secondNode);

            firstParent.children[indexOfFirstChild] = secondNode;
            secondParent.children[indexOfSecondChild] = firstNode;

            firstNode.parent = secondParent;
            secondNode.parent = firstParent;
        }
    }
}