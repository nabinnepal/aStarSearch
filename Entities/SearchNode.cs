using System;
using System.Collections.Generic;
using System.Text;

namespace AStarSearchImplementation
{
    public class SearchNode : INode
    {
        public SearchNode()
        {}
        public SearchNode(Location location, INode parent)
        {
            Location = location;
            Parent = parent;
        }
        #region INode Members

        public Location Location { get; set; }

        public INode Parent { get; set; }

        #endregion
        
    }
}
