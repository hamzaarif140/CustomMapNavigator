using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Datatypes;

namespace NEA.Interfaces
{

    //  Classes inheriting from this interface must provide a method to find a path given a start node, an end node, and a grid graph.
    internal interface IPathFinder
    {

        List<PixelNode> FindPath(PixelNode start, PixelNode end, GridGraph graph);
    }
}
