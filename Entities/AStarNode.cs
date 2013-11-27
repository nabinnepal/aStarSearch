namespace AStarSearchImplementation
{
    public class AStarNode : SearchNode
    {
        #region Constructor
        public AStarNode() { }

        public AStarNode(Location location, INode parent,
            decimal costFromStart, decimal costToGoal)
            : base(location, parent)
        {
            CostFromStart = costFromStart;
            CostToGoal = costToGoal;
        } 
        #endregion

        #region Properties
        
        public decimal CostFromStart { get; set; }
        public decimal CostToGoal { get; set; }
        public decimal TotalCost { get { return CostFromStart + CostToGoal; } }
       
        #endregion
    }
}
