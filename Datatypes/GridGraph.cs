using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Pathfinding;
using NEA.Interfaces;
using NEA.Processing;
using NEA.Utility;
   

namespace NEA.Datatypes

{
    internal class GridGraph
    {
        // Dictionary mapping coordinates to PixelNode.
        public Dictionary<Point, PixelNode> Nodes { get; private set; }

        public GridGraph()
        {
            Nodes = new Dictionary<Point, PixelNode>();
        }

        // Build the graph from a list of PixelNodes.
        public void BuildGraph(List<PixelNode> nodeList)
        {
            // Populate the dictionary.
            foreach (var node in nodeList)
            {
                Point point = new Point(node.X, node.Y);
                if (!Nodes.ContainsKey(point))
                    Nodes.Add(point, node);
            }

            // For each node, check all 8 neighbours.
            foreach (var keyValuePair in Nodes)
            {
                PixelNode current = keyValuePair.Value;
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx == 0 && dy == 0)
                            continue;

                        Point neighbourCoords = new Point(current.X + dx, current.Y + dy);
                        if (Nodes.ContainsKey(neighbourCoords))
                        {
                            // Determine cost: straight moves cost 1, diagonal moves cost root 2.
                            double cost;

                            if (dx == 0 || dy == 0)
                            {
                                cost = 1.0; 
                            }
                            else
                            {
                                cost = Math.Sqrt(2);
                            }
                            current.Neighbours.Add(new Tuple<PixelNode, double>(Nodes[neighbourCoords], cost));
                        }
                    }
                }
            }
        }

    }
}
