using System;
using System.Collections.Generic;

namespace AStarSearchImplementation
{
    public class AStarSearch
    {
        #region Fields
        private Dictionary<Location, Node> open;
        private Dictionary<Location, Node> closed;

        private int[,] grid;
        private Location startLoc, goalLoc; 
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="startLoc"></param>
        /// <param name="goalLoc"></param>
        public AStarSearch(int[,] grid, Location startLoc, Location goalLoc)
        {
            this.grid = grid;
            this.startLoc = startLoc;
            this.goalLoc = goalLoc;
            open = new Dictionary<Location, Node>(grid.Length);
            closed = new Dictionary<Location, Node>(grid.Length);
        } 
        #endregion

        #region Methods
        public List<Node> ExecuteSearch()
        {
            PriorityQueue openQ = new PriorityQueue();
            Node startNode = new Node();
            startNode.Location = startLoc;
            startNode.CostFromStart = 0;
            startNode.CostToGoal = FindHeuristic(startLoc, goalLoc);
            startNode.Parent = null;

            openQ.Enqueue(startNode);
            open.Add(startNode.Location, startNode);

            while (openQ.Count > 0)
            {
                Node node = openQ.Dequeue();
                open.Remove(node.Location);
                
                if (node.Location.Equals(goalLoc))
                    return GetSolution(node);
                
                List<Node> neighbors = FindNeighbors(node);
                foreach (Node newNode in neighbors)
                {
                    Location nnLoc = newNode.Location;
                    if (closed.ContainsKey(nnLoc)) continue;

                    decimal newCostFromStart = node.CostFromStart + FindActualCost(node.Location, newNode.Location);
                    decimal newCostToGoal = FindHeuristic(newNode.Location, goalLoc);
                    decimal newTotalCost = newCostFromStart + newCostToGoal;
                    
                    Node holderO = null;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <returns></returns>
        private static decimal FindActualCost(Location location1, Location location2)
        {
            return (decimal)Math.Sqrt(Math.Abs(location2.X - location1.X) + 
                Math.Abs(location2.Y - location1.Y));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static List<Node> GetSolution(Node node)
        {
            var pathList = new List<Node> {node};
            while (node.Parent != null)
            {
                pathList.Insert(0, node.Parent);
                node = node.Parent;
            }
            return pathList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<Node> FindNeighbors(Node node)
        {
            Location nodeLoc = node.Location;
            
            var neighbors = new List<Node>();

            AddConditional(neighbors, nodeLoc, -1, 0);
            AddConditional(neighbors, nodeLoc, 0, -1);
            AddConditional(neighbors, nodeLoc, 0, 1);
            AddConditional(neighbors, nodeLoc, 1, 0);

            return neighbors;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addTo"></param>
        /// <param name="loc"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void AddConditional(IList<Node> addTo, Location loc, int x, int y)
        {
            int newX = loc.X + x, newY = loc.Y + y;
            if (newX < 0 || newX >= grid.GetLength(0))
            {
                return;
            }
            if (newY < 0 || newY >= grid.GetLength(1))
            {
                return;
            }
            if (grid[newX, newY] == Constants.SOLID)
                return;

            var location = new Location(newX, newY);
            
            var newNode = new Node() {Location = location};
            addTo.Add(newNode);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startLoc"></param>
        /// <param name="goalLoc"></param>
        /// <returns></returns>
        private static decimal FindHeuristic(Location startLoc, Location goalLoc)
        {
            decimal dx = Math.Abs(goalLoc.X - startLoc.X);
            decimal dy = Math.Abs(goalLoc.Y - startLoc.Y);
            return ((decimal)0.5 * (dx + dy));
        } 
        #endregion
    }
}
