using System.Collections.Generic;
using System.Linq;

namespace AStarSearchImplementation
{
    public class BFSSearchStrategy : SearchStrategyBase<SearchNode>
    {
        #region ISearchStrategy Members

        public override INode GetResult(Location startLocation, Location goalLocation, 
            int[,] grid)
        {
            var closed = new Dictionary<Location, SearchNode>();

            var openQ = new Queue<SearchNode>();
            var startNode = new SearchNode(startLocation, null);

            openQ.Enqueue(startNode);

            while (openQ.Count > 0)
            {
                SearchNode node = openQ.Dequeue();
            
                if (node.Location.Equals(goalLocation))
                    return node;

                IncrementNodesExplored();

                var neighbors = FindNeighbors(node, grid);
                foreach (SearchNode newNode in neighbors)
                {
                    if (closed.ContainsKey(newNode.Location)) 
                        continue;

                    var nodevalue = openQ.FirstOrDefault(s => s.Location.Equals(newNode.Location));

                    if(nodevalue == null)
                    {
                        newNode.Parent = node;
                        openQ.Enqueue(newNode);    
                    }
                }
                closed.Add(node.Location, node);
            }
            return null;
        }

        #endregion

        
    }
}
