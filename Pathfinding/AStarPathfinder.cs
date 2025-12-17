using NEA.Datatypes;
using NEA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.Pathfinding
{

        internal class AStarPathFinder : IPathFinder
        {
            public List<PixelNode> FindPath(PixelNode start, PixelNode end, GridGraph graph)
            {
            // for every PixelNode in the graph create AStarNode
            Dictionary<PixelNode, AStarNode> nodeRecords = new Dictionary<PixelNode, AStarNode>();
                foreach (var keyValuePair in graph.Nodes)
                {
                    nodeRecords[keyValuePair.Value] = new AStarNode(keyValuePair.Value);
                }

            // Initialise the start node
            AStarNode startRecord = nodeRecords[start];
            startRecord.GScore = 0;
                startRecord.HScore = Heuristic(start, end);

            // Open list: nodes to be evaluated, prioritised by lowest FScore
            MinPriorityQueue<AStarNode> openList = new MinPriorityQueue<AStarNode>();
            openList.Enqueue(startRecord, startRecord.FScore);

            // Closed list: nodes already fully evaluated
            HashSet<PixelNode> closedList = new HashSet<PixelNode>();

            while (openList.Count > 0)
                {
                    // Take the node with the lowest FScore
                    var current = openList.Dequeue();

                    // If we've reached the goal, rebuild and return the path
                    if (current.Node == end)
                        return ReconstructPath(current);

                    // Mark this node as evaluated
                    closedList.Add(current.Node);

                    // Examine each neighbour of the current node
                    foreach (var (neighbourPixel, cost) in current.Node.Neighbours)
                    {
                        // Skip if we've already evaluated this neighbour
                        if (closedList.Contains(neighbourPixel))
                            continue;

                        var neighbourRecord = nodeRecords[neighbourPixel];
                        double newGScore = current.GScore + cost;

                        // If this path to neighbour is better, record it
                        if (newGScore < neighbourRecord.GScore)
                        {
                            neighbourRecord.CameFrom = current;
                            neighbourRecord.GScore = newGScore;
                            neighbourRecord.HScore = Heuristic(neighbourPixel, end);

                            // If neighbour isnt in open set, add it. Otherwise update its priority
                            if (!openList.Contains(neighbourRecord))
                            {
                                openList.Enqueue(neighbourRecord, neighbourRecord.FScore);
                            }
                            else
                            {
                                openList.UpdatePriority(neighbourRecord, neighbourRecord.FScore);
                            }
                        }
                    }
                }

                // No path found
                return new List<PixelNode>();
            }

            // Heuristic function: straight‑line (Euclidean) distance between two pixels
            private double Heuristic(PixelNode a, PixelNode b)
            {
                double dx = a.X - b.X;
                double dy = a.Y - b.Y;
                return Math.Sqrt(dx * dx + dy * dy);
            }

            // Reconstructs the path by following CameFrom pointers back to the start
            private List<PixelNode> ReconstructPath(AStarNode endRecord)
            {
            List<PixelNode> path = new List<PixelNode>();
            AStarNode current = endRecord;
            while (current != null)
                {
                    path.Add(current.Node);
                    current = current.CameFrom;
                }
                path.Reverse();  //  built it from the end node to the start, so reverse it
                return path;
            }
        }
}
