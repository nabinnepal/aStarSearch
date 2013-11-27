using System;
using System.Collections.Generic;
using System.Text;

namespace AStarSearchImplementation
{
    public class Node
    {
        #region Methods
        public override bool Equals(object obj)
        {
            if (obj is Node)
            {
                return Location.Equals(((Node)obj).Location);
            }
            return false;
            
        }
        public override int GetHashCode()
        {
            return Location.GetHashCode();
        } 
        #endregion

        #region Properties
        public decimal CostFromStart
        {
            get; set;
        }
        public decimal CostToGoal
        {
            get;
            set;
        }

        public decimal TotalCost
        {
            get { return CostFromStart + CostToGoal; }
        }
        public Location Location
        {
            get;
            set;
        }
        public Node Parent
        {
            get;
            set;
        } 
        #endregion
    }
}
