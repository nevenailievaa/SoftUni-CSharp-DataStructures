namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class IntegerTree : Tree<int>, IIntegerTree
    {
        //Constructor
        public IntegerTree(int key, params Tree<int>[] children) : base(key, children) { }

        //DFS 
        public IEnumerable<IEnumerable<int>> GetPathsWithGivenSum(int sum)
        {
            var result = new List<List<int>>();
            var currentPath = new LinkedList<int>();
            currentPath.AddFirst(Key);

            int currentSum = Key;
            Dfs(this, result, currentPath, ref currentSum, sum);

            return result;
        }

        private void Dfs(Tree<int> subtree, List<List<int>> result, LinkedList<int> currentPath, ref int currentSum, int wantedSum)
        {
            foreach (var child in subtree.Children)
            {
                currentSum += child.Key;
                currentPath.AddLast(child.Key);
                Dfs(child, result, currentPath, ref currentSum, wantedSum);
            }

            if (currentSum == wantedSum)
            {
                result.Add(new List<int>(currentPath));
            }

            currentSum -= subtree.Key;
            currentPath.RemoveLast();
        }

        //BFS
        public IEnumerable<Tree<int>> GetSubtreesWithGivenSum(int wantedSum)
        {
            var result = new List<Tree<int>>();
            var allSubtrees = this.GetAllNodesBfs();

            foreach (var subtree in allSubtrees)
            {
                if (this.SubtreeHasWantedSum(subtree, wantedSum))
                {
                    result.Add(subtree);
                }
            }
            return result;
        }

        private bool SubtreeHasWantedSum(Tree<int> subtree, int wantedSum)
        {
            int sum = subtree.Key;
            this.DfsGetSubtreeSum(subtree, wantedSum, ref sum);
            return sum == wantedSum;
        }

        private void DfsGetSubtreeSum(Tree<int> subtree, int wantedSum, ref int sum)
        {
            foreach (var child in subtree.Children)
            {
                sum += child.Key;
                this.DfsGetSubtreeSum(child, wantedSum, ref sum);
            }
        }

        private IEnumerable<Tree<int>> GetAllNodesBfs()
        {
            var queue = new Queue<Tree<int>>();
            var result = new List<Tree<int>>();

            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var subtree = queue.Dequeue();
                result.Add(subtree);

                foreach (var child in subtree.Children)
                {
                    queue.Enqueue(child);
                }
            }
            return result;
        }
    }
}
