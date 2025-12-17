using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Pathfinding;

namespace NEA
{


    internal class Settings
    {
        // Enum representing available pathfinding algorithms.
        public enum PathfindingAlgorithm
        {
            Dijkstra,
            AStar
        }

        // Stores the currently selected pathfinding algorithm.
        // Default is A star.
        public static PathfindingAlgorithm SelectedPathfindingAlgorithm { get; set; } = PathfindingAlgorithm.AStar;

      
    }
}
