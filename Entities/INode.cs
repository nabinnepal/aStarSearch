namespace AStarSearchImplementation
{
    public interface INode
    {
        Location Location { get; set; }
        INode Parent { get; set; }
    }
}
