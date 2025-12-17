using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Pathfinding;

namespace NEA.Datatypes
{
        internal class PixelNode
        {
        public int X { get; set; }
        public int Y { get; set; }
        // List of neighbours along with the cost to travel to them.
        public List<Tuple<PixelNode, double>> Neighbours { get; set; }

        public PixelNode(int x, int y)
        {
            X = x;
            Y = y;
            Neighbours = new List<Tuple<PixelNode, double>>();
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
