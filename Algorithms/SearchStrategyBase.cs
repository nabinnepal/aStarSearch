using System.Collections.Generic;

namespace AStarSearchImplementation
{
    public abstract class SearchStrategyBase<T> : ISearchStrategy where  T:INode, new()
    {
        #region Fields
        private int _noOfNodes; 
        #endregion

        #region ISearchStrategy Members

        public abstract INode GetResult(Location startLocation, 
            Location goalLocation, int[,] grid);

        public int NoOfNodesExplored
        {
            get { return _noOfNodes; }
        }

        #endregion

        #region Protected Methods
        protected List<INode> FindNeighbors(INode node, int[,] grid)
        {
            Location nodeLoc = node.Location;

            var neighbors = new List<INode>();

            AddConditional(neighbors, nodeLoc, grid, -1, 0);
            AddConditional(neighbors, nodeLoc, grid, 0, -1);
            AddConditional(neighbors, nodeLoc, grid, 0, 1);
            AddConditional(neighbors, nodeLoc, grid, 1, 0);

            return neighbors;
        }

        protected void AddConditional(IList<INode> addTo, Location loc, 
            int[,] grid, int x, int y)
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

            var newNode = new T() { Location = location };
            addTo.Add(newNode);
        }
 
        protected void IncrementNodesExplored()
        {
            _noOfNodes++;
        }
        #endregion
    }
}
