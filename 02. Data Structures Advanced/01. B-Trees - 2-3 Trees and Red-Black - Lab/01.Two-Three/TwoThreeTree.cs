namespace _01.Two_Three
{
    using System;
    using System.Text;

    public class TwoThreeTree<T> where T : IComparable<T>
    {
        private TreeNode<T> root;

        public void Insert(T element)
        {
            root = Insert(root, element);
        }

        public TreeNode<T> Insert(TreeNode<T> node, T element)
        {
            //when tree is empty
            if (node == null)
            {
                return new TreeNode<T>(element);
            }

            //reached end of tree
            if (node.IsLeaf())
            {
                return MergeNodes(node, new TreeNode<T>(element));
            }

            if (IsLesser(element, node.LeftKey))
            {
                var newNode = Insert(node.LeftChild, element);
                return newNode == node.LeftChild ? node : MergeNodes(node, newNode);
            }
            else if (node.IsTwoNode() || IsLesser(element, node.RightKey))
            {
                var newNode = Insert(node.MiddleChild, element);
                return newNode == node.MiddleChild ? node : MergeNodes(node, newNode);
            }
            else
            {
                var newNode = Insert(node.RightChild, element);
                return newNode == node.RightChild ? node : MergeNodes(node, newNode);
            }
        }

        private bool IsLesser(T item, T compareItem)
        {
            return item.CompareTo(compareItem) < 0;
        }

        private TreeNode<T> MergeNodes(TreeNode<T> current, TreeNode<T> node)
        {
            //2-node to 3-node
            if (current.IsTwoNode())
            {
                if (IsLesser(current.LeftKey, node.LeftKey))
                {
                    current.RightKey = node.LeftKey;
                    current.MiddleChild = node.LeftChild;
                    current.RightChild = node.MiddleChild;
                }
                else
                {
                    current.RightKey = current.LeftKey;
                    current.RightChild = current.MiddleChild;
                    current.MiddleChild = node.MiddleChild;
                    current.LeftChild = node.LeftChild;
                    current.LeftKey = node.LeftKey;
                }
                return current;
            }

            //new node
            else if (IsLesser(node.LeftKey, current.LeftKey))
            {
                var newNode = new TreeNode<T>(current.LeftKey)
                {
                    LeftChild = node,
                    MiddleChild = current
                };
                current.LeftChild = current.MiddleChild;
                current.MiddleChild = current.RightChild;
                current.LeftKey = current.RightKey;
                current.RightKey = default;
                current.RightChild = null;

                return newNode;
            }
            else if (IsLesser(node.LeftKey, current.RightKey))
            {
                node.MiddleChild = new TreeNode<T>(current.RightKey)
                {
                    LeftChild = node.MiddleChild,
                    RightChild = current.RightChild
                };
                node.LeftChild = current;
                current.RightKey = default;
                current.RightChild = null;
                return node;
            }

            else
            {
                var newNode = new TreeNode<T>(current.RightKey)
                {
                    LeftChild = current,
                    MiddleChild = node
                };

                node.LeftChild = current.RightChild;
                current.RightKey = default;
                current.RightChild = null;

                return newNode;
            }

        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            RecursivePrint(this.root, sb);
            return sb.ToString();
        }

        private void RecursivePrint(TreeNode<T> node, StringBuilder sb)
        {
            if (node == null)
            {
                return;
            }

            if (node.LeftKey != null)
            {
                sb.Append(node.LeftKey).Append(" ");
            }

            if (node.RightKey != null)
            {
                sb.Append(node.RightKey).Append(Environment.NewLine);
            }
            else
            {
                sb.Append(Environment.NewLine);
            }

            if (node.IsTwoNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.MiddleChild, sb);
            }
            else if (node.IsThreeNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.MiddleChild, sb);
                RecursivePrint(node.RightChild, sb);
            }
        }
    }
}