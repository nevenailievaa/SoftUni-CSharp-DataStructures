namespace _02.LowestCommonAncestor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        //Fields
        private readonly List<T> path1 = new List<T>();
        private readonly List<T> path2 = new List<T>();

        //Constructor
        public BinaryTree(
            T value,
            BinaryTree<T> leftChild,
            BinaryTree<T> rightChild)
        {
            Value = value;
            LeftChild = leftChild;
            RightChild = rightChild;
            if (leftChild != null)
            {
                LeftChild.Parent = this;
            }

            if (rightChild != null)
            {
                RightChild.Parent = this;
            }
        }

        //Properties
        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        //Methods
        public T FindLowestCommonAncestor(T first, T second)
        {
            path1.Clear();
            path2.Clear();
            return FindLowestCommonAncestor(this, first, second);
        }

        private T FindLowestCommonAncestor(BinaryTree<T> node, T first, T second)
        {
            if (!FindPath(node, first, path1) || !FindPath(node, second, path2))
            {
                throw new InvalidOperationException();
            }
            int i;
            for (i = 0; i < path1.Count && i < path2.Count; i++)
            {
                if (!path1[i].Equals(path2[i]))
                {
                    break;
                }
            }
            return path1[i - 1];
        }

        private bool FindPath(BinaryTree<T> node, T element, List<T> path)
        {
            if (node == null)
            {
                return false;
            }

            path.Add(node.Value);

            if (node.Value.Equals(element))
            {
                return true;
            }
            if (node.LeftChild != null && FindPath(node.LeftChild, element, path))
            {
                return true;
            }
            if(node.RightChild != null && FindPath(node.RightChild, element, path))
            {
                return true;
            }

            path.RemoveAt(path.Count - 1);
            return false;
        }
    }
}