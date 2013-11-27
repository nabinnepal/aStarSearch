using System;
using System.Collections.Generic;

namespace AStarSearchImplementation
{
    public class PathFinder
    {
        #region Fields

        private readonly ISearchStrategy _searchStrategy;
        private readonly int[,] _grid;
        private readonly Location _startLoc, _goalLoc; 
        #endregion

        #region Constructor
        
        public PathFinder(int[,] grid, Location startLoc, Location goalLoc, 
            ISearchStrategy searchStrategy)
        {
            _grid = grid;
            _startLoc = startLoc;
            _goalLoc = goalLoc;
            _searchStrategy = searchStrategy;
        } 
        #endregion

        #region Methods
        public List<INode> ExecuteSearch()
        {
            var node = _searchStrategy.GetResult(_startLoc, _goalLoc, _grid);

            return node == null ? null : GetSolution(node);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static List<INode> GetSolution(INode node)
        {
            var pathList = new List<INode> {node};
            while (node.Parent != null)
            {
                pathList.Insert(0, node.Parent);
                node = node.Parent;
            }
            return pathList;
        }
        
        #endregion
    }
}
