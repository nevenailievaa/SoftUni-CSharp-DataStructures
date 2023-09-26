namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        //Constructor
        public BinaryTree(T element, IAbstractBinaryTree<T> left, IAbstractBinaryTree<T> right)
        {
            Value = element;
            LeftChild = left;
            RightChild = right;
        }

        //Properties
        public T Value { get; private set; }

        public IAbstractBinaryTree<T> LeftChild { get; private set; }

        public IAbstractBinaryTree<T> RightChild { get; private set; }

        //Methods
        public string AsIndentedPreOrder(int indent)
        {
            StringBuilder sb = new StringBuilder();
            PreOrderDfs(sb, indent, this);

            return sb.ToString();
        }

        private void PreOrderDfs(StringBuilder sb, int indent, IAbstractBinaryTree<T> tree)
        {
            sb.Append(' ', indent);
            sb.AppendLine(tree.Value.ToString());

            if (tree.LeftChild != null)
            {
                PreOrderDfs(sb, indent + 2, tree.LeftChild);
            }
            if (tree.RightChild != null)
            {
                PreOrderDfs(sb, indent + 2, tree.RightChild);
            }
        }

        public void ForEachInOrder(Action<T> action)
        {
            var inOrderTrees = InOrder();

            foreach (var tree in inOrderTrees)
            {
                action.Invoke(tree.Value);
            }
        }

        public IEnumerable<IAbstractBinaryTree<T>> InOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            if (LeftChild != null)
            {
                result.AddRange(LeftChild.InOrder());
            }

            result.Add(this);

            if (RightChild != null)
            {
                result.AddRange(RightChild.InOrder());
            }

            return result;
        }

        public IEnumerable<IAbstractBinaryTree<T>> PostOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            if (LeftChild != null)
            {
                result.AddRange(LeftChild.PostOrder());
            }
            if (RightChild != null)
            {
                result.AddRange(RightChild.PostOrder());
            }

            result.Add(this);
            return result;
        }

        public IEnumerable<IAbstractBinaryTree<T>> PreOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            result.Add(this);

            if (LeftChild != null)
            {
                result.AddRange(LeftChild.PreOrder());
            }
            if (RightChild != null)
            {
                result.AddRange(RightChild.PreOrder());
            }

            return result;
        }
    }
}
