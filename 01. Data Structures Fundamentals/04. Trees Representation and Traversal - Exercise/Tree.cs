namespace Tree
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        //Fields
        private List<Tree<T>> children;

        //Constructor
        public Tree(T key, params Tree<T>[] children)
        {
            Key = key;
            this.children = new List<Tree<T>>();

            foreach (var child in children)
            {
                this.AddChild(child);
                child.Parent = this;
            }
        }

        //Properties
        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children => children;

        //Methods
        public void AddChild(Tree<T> child)
        {
            children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            Parent = parent;
        }

        public string GetAsString()
        {
            StringBuilder sb = new StringBuilder();

            DfsAsString(sb, this, 0);

            return sb.ToString().Trim();
        }
        private void DfsAsString(StringBuilder sb, Tree<T> tree, int depth)
        {
            sb.Append(' ', depth);
            sb.AppendLine(tree.Key.ToString());

            foreach (var child in tree.children)
            {
                DfsAsString(sb, child, depth + 2);
            }
        }

        public IEnumerable<T> GetInternalKeys()
        {
            return BfsWithResultKeys(tree => tree.children.Count > 0 && tree.Parent != null).Select(tree => tree.Key);
        }

        public IEnumerable<T> GetLeafKeys()
        {
            return BfsWithResultKeys(tree => tree.children.Count == 0).Select(tree => tree.Key);
        }

        private IEnumerable<Tree<T>> BfsWithResultKeys(Predicate<Tree<T>> predicate)
        {
            var queue = new Queue<Tree<T>>();
            List<Tree<T>> result = new List<Tree<T>>();

            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var currentSubtree = queue.Dequeue();

                if (predicate.Invoke(currentSubtree))
                {
                    result.Add(currentSubtree);
                }

                foreach (var child in currentSubtree.children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public T GetDeepestKey()
        {
            var value = GetDeepestNode();

            return value.Key;
        }
        private Tree<T> GetDeepestNode()
        {
            var leafs = BfsWithResultKeys(tree => tree.Children.Count == 0);
            Tree<T> deepestNode = null;
            int deepestNodeDepth = int.MinValue;

            foreach (var leaf in leafs)
            {
                int depth = GetDepth(leaf);

                if (depth > deepestNodeDepth)
                {
                    deepestNodeDepth = depth;
                    deepestNode = leaf;
                }
            }
            return deepestNode;
        }

        private int GetDepth(Tree<T> tree)
        {
            int depth = 0;
            Tree<T> currentLeaf = tree;

            while (currentLeaf.Parent != null)
            {
                depth++;
                currentLeaf = currentLeaf.Parent;
            }

            return depth;
        }

        public IEnumerable<T> GetLongestPath()
        {
            Tree<T> currentNode = GetDeepestNode();
            List<T> treePath = new List<T>();

            while (currentNode.Parent != null)
            {
                treePath.Add(currentNode.Key);
                currentNode = currentNode.Parent;
            }
            treePath.Add(currentNode.Key);

            treePath.Reverse();
            return treePath;
        }
    }
}
