using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AStarSearchImplementation
{
    public class DFSSearchStrategy : SearchStrategyBase<SearchNode>
    {
        #region ISearchStrategy Members

        public override INode GetResult(Location startLocation, Location goalLocation, int[,] grid)
        {
            var closed = new Dictionary<Location, SearchNode>();

            var openStack = new Stack<SearchNode>();
            var startNode = new SearchNode(startLocation, null);

            openStack.Push(startNode);

            while (openStack.Count > 0)
            {
                SearchNode node = openStack.Pop();

                if (node.Location.Equals(goalLocation))
                    return node;

                IncrementNodesExplored();

                var neighbors = FindNeighbors(node, grid);
                foreach (SearchNode newNode in neighbors)
                {
                    if (closed.ContainsKey(newNode.Location))
                        continue;

                    var nodevalue = openStack.FirstOrDefault(s => s.Location.Equals(newNode.Location));

                    if (nodevalue == null)
                    {
                        newNode.Parent = node;
                        openStack.Push(newNode);
                    }
                }
                closed.Add(node.Location, node);
            }
            return null;
        }

        #endregion
    }
}
