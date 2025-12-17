using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Datatypes;
using NEA.Interfaces;

namespace NEA.Pathfinding
{
    internal class DijkstraPathfinder : IPathFinder
    {

        public List<PixelNode> FindPath(PixelNode start, PixelNode end, GridGraph graph)
        {
            // Create dictionaries to store the current shortest distance and the previous node.
            Dictionary<PixelNode, double> distances = new Dictionary<PixelNode, double>();
            Dictionary<PixelNode, PixelNode> previous = new Dictionary<PixelNode, PixelNode>();
            HashSet<PixelNode> unvisited = new HashSet<PixelNode>();

            // Initialise distances for each node in the graph.
            foreach (var node in graph.Nodes.Values)
            {
                distances[node] = double.PositiveInfinity;
                previous[node] = null;
                unvisited.Add(node);
            }

            // Set the start node's distance to zero.
            distances[start] = 0;

            while (unvisited.Count > 0)
            {
                // Select the unvisited node with the smallest distance.
                PixelNode current = null;
                double minDistance = double.PositiveInfinity;
                foreach (var node in unvisited)
                {
                    if (distances[node] < minDistance)
                    {
                        minDistance = distances[node];
                        current = node;
                    }
                }

                // If the target is reached or no accessible node remains, break.
                if (current == null || current.X == end.X && current.Y == end.Y)
                    break;

                unvisited.Remove(current);

                // Update distances for neighbours.
                foreach (var neighbourInfo in current.Neighbours)
                {
                    PixelNode neighbour = neighbourInfo.Item1;
                    double cost = neighbourInfo.Item2;

                    if (!unvisited.Contains(neighbour))
                        continue;

                    double altDistance = distances[current] + cost;
                    if (altDistance < distances[neighbour])
                    {
                        distances[neighbour] = altDistance;
                        previous[neighbour] = current;
                    }
                }
            }

            // Reconstruct the shortest path by backtracking from end to start.
            List<PixelNode> path = new List<PixelNode>();
            PixelNode currentNode = end;
            while (currentNode != null)
            {
                path.Add(currentNode);
                currentNode = previous[currentNode];
            }
            path.Reverse();
            return path;
        }
    }
}
