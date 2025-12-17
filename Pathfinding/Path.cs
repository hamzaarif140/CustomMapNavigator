using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Datatypes;


namespace NEA.Pathfinding
{

    internal class Path
    {
        // The list of nodes in the computed path
        public List<PixelNode> Nodes { get; private set; }
        // The total cost of travelling along the path
        public double TotalCost { get; private set; }

        // Constructor initialises the path with nodes and the total cost
        public Path(List<PixelNode> nodes, double totalCost)
        {
            Nodes = nodes;
            TotalCost = totalCost;
        }

    }
}
