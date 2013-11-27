using System.Collections.Generic;

namespace AStarSearchImplementation
{
    public class PriorityQueue
    {
        #region Fields
        private readonly List<Node> nodes; 
        #endregion

        #region Constructor
        public PriorityQueue()
        {
            nodes = new List<Node>();
        } 
        #endregion

        #region Properties
        public int Count
        {
            get { return nodes.Count; }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// This method removes the first element from the queue
        /// </summary>
        /// <returns></returns>
        public Node Dequeue()
        {
            Node node = nodes[0];
            nodes.RemoveAt(0);
            return node;
        }

        /// <summary>
        /// This method adds the node to the list based on totalcost of the node
        /// </summary>
        /// <param name="node"></param>
        public void Enqueue(Node node)
        {
            bool found = false;
            for (int i = 0; i < nodes.Count; i++)
            {
                Node holder = nodes[i];
                if (holder.TotalCost >= node.TotalCost)
                {
                    nodes.Insert(i, node);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                nodes.Add(node);
            }

        }
        /// <summary>
        /// This method checks whether the Priority queue contains the node or not
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Contains(Node node)
        {
            return nodes.Contains(node);
        }
        /// <summary>
        /// This function removes the element from the Priority queue
        /// </summary>
        /// <param name="node"></param>
        public void Remove(Node node)
        {
            nodes.Remove(node);
        } 
        #endregion
    }
}
