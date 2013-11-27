namespace AStarSearchImplementation
{
    public interface ISearchStrategy
    {
        INode GetResult(Location startLocation, Location goalLocation, int[,] grid);
        int NoOfNodesExplored { get; }
    }
}
