using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Datatypes;
using NEA.Interfaces;
using NEA.Pathfinding;

namespace NEA.Pathfinding
{
    // This class controls pathfinding process
    // It takes a grid graph, user-selected start and end points and calls the path fiunding algorithm. Also visualises the route
    internal class RoutePlanner
    {
        private GridGraph gridGraph;
        private IPathFinder pathfinder;
        private PathVisualiser visualiser;


        public RoutePlanner(GridGraph graph, IPathFinder pathfinder)
        {
            this.gridGraph = graph;
            this.pathfinder = pathfinder;
            this.visualiser = new PathVisualiser();
        }

        // Maps a point to the nearest node
        public PixelNode MapPointToNode(Point p)
        {
            PixelNode nearestNode = null;
            double minimumDistance = double.PositiveInfinity;
            foreach (var nodeEntry in gridGraph.Nodes)
            {
                PixelNode node = nodeEntry.Value;
                double distance = Math.Sqrt(Math.Pow(node.X - p.X, 2) + Math.Pow(node.Y - p.Y, 2));
                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    nearestNode = node;
                }
            }
            return nearestNode;
        }

        // Uses the pathfinder to compute the shortest path from the start node to the end node.
        public Path PlanRoute(PixelNode start, PixelNode end)
        {
            List<PixelNode> nodes = pathfinder.FindPath(start, end, gridGraph);
            double totalPathCost = CalculatePathCost(nodes);
            return new Path(nodes, totalPathCost);
        }

        // calculates the total cost along a path by adding edge costs
        private double CalculatePathCost(List<PixelNode> nodes)
        {
            double totalCost = 0;
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                PixelNode current = nodes[i];
                PixelNode next = nodes[i + 1];
                // Find the cost between current and next node by checking the neighbours.
                foreach (var neighbour in current.Neighbours)
                {
                    if (neighbour.Item1.X == next.X && neighbour.Item1.Y == next.Y)
                    {
                        totalCost += neighbour.Item2;
                        break;
                    }
                }
            }
            return totalCost;
        }

        // shows the computed route on the provided map image.
        public Bitmap VisualiseRoute(Path route, Bitmap map)
        {
            return visualiser.VisualisePath(map, route, Color.Blue);
        }
    }
}
