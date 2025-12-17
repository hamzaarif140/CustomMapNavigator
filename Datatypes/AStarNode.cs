using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.Datatypes
{
    // This class holds one PixelNode plus the extra data A star needs
    internal class AStarNode
    {
        // The underlying graph node (pixel coordinates)
        public PixelNode Node { get; }

        //  The exact cost from the start node to this node
        public double GScore { get; set; }

        // Heuristic estimate of cost from this node to the end node
        public double HScore { get; set; }

        // Total estimated cost (GScore + HScore)
        public double FScore
        {
            get { return GScore + HScore; }
        }

        // Back‑pointer to rebuild the final path
        public AStarNode CameFrom { get; set; }

        // Wrap a PixelNode and initialise all scores
        public AStarNode(PixelNode node)
        {
            Node = node;
            GScore = double.PositiveInfinity;  // start unknown, so infinite
            HScore = 0;                        // will be set when known
            CameFrom = null;                   // no predecessor yet
        }
    }
}

