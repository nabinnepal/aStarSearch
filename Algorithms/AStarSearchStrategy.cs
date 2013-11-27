using System;
using System.Collections.Generic;
using System.Text;

namespace AStarSearchImplementation
{
    public class AStarSearchStrategy : SearchStrategyBase<AStarNode>
    {
        #region ISearchStrategy Members

        public override INode GetResult(Location startLocation, 
            Location goalLocation, int[,] grid)
        {
            var open =new Dictionary<Location, AStarNode>();
            var closed = new Dictionary<Location, AStarNode>();
            
            var openQ = new PriorityQueue();
            var startNode = new AStarNode() { Location = startLocation, CostFromStart = 0, Parent = null };
            startNode.CostToGoal = FindHeuristic(startLocation, goalLocation);
            
            openQ.Enqueue(startNode);
            open.Add(startNode.Location, startNode);

            while (openQ.Count > 0)
            {
                AStarNode node = openQ.Dequeue();
                open.Remove(node.Location);

                if (node.Location.Equals(goalLocation))
                    return node;

                IncrementNodesExplored();

                var neighbors = FindNeighbors(node, grid);
                foreach (AStarNode newNode in neighbors)
                {
                    if (closed.ContainsKey(newNode.Location)) 
                        continue;

                    decimal newCostFromStart = node.CostFromStart + FindActualCost(node.Location, newNode.Location);
                    decimal newCostToGoal = FindHeuristic(newNode.Location, goalLocation);
                    decimal newTotalCost = newCostFromStart + newCostToGoal;

                    AStarNode holderO = null;
                    open.TryGetValue(newNode.Location, out holderO);

                    if (holderO != null && holderO.TotalCost <= newTotalCost)
                        continue;

                    newNode.CostFromStart = newCostFromStart;
                    newNode.CostToGoal = newCostToGoal;
                    newNode.Parent = node;

                    if (holderO != null) //remove old location if it exists
                    {
                        openQ.Remove(holderO);
                        open.Remove(newNode.Location);
                    }
                    openQ.Enqueue(newNode);
                    open.Add(newNode.Location, newNode);

                }
                closed.Add(node.Location, node);
            }
            return null;
        }
        #endregion

        private static decimal FindActualCost(Location location1, Location location2)
        {
            return (decimal)Math.Sqrt(Math.Abs(location2.X - location1.X) +
                Math.Abs(location2.Y - location1.Y));
        }
        
        
        private static decimal FindHeuristic(Location startLoc, Location goalLoc)
        {
            decimal dx = Math.Abs(goalLoc.X - startLoc.X);
            decimal dy = Math.Abs(goalLoc.Y - startLoc.Y);
            return dx + dy;
        }

        
    }
}
