using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Datatypes;

namespace NEA.Pathfinding
{
    // This class  draws the computed path onto a map image.
    internal class PathVisualiser
    {

        // shows the path by drawing a coloured line through the sequence of nodes.
        public Bitmap VisualisePath(Bitmap map, Path path, Color pathColour, int thickness = 1)
        {
            // Check that the inputs are valid.
            if (map == null)
                throw new ArgumentNullException("map");
            if (path == null || path.Nodes.Count == 0)
                throw new ArgumentException("Path is empty.", "path");

            Bitmap output = new Bitmap(map);
            using (Graphics g = Graphics.FromImage(output))
            {
                // smoother lines
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Create a pen to draw the path with the specified colour and thickness.
                using (Pen pen = new Pen(pathColour, thickness))
                {
                    // Convert the list of nodes into an array of points.
                    Point[] points = new Point[path.Nodes.Count];
                    for (int i = 0; i < path.Nodes.Count; i++)
                    {
                        points[i] = new Point(path.Nodes[i].X, path.Nodes[i].Y);
                    }
                    // If there are at least two points, draw a connected line through them.
                    if (points.Length > 1)
                    {
                        g.DrawLines(pen, points);
                    }
                }
                //  mark the start and end nodes 
                if (path.Nodes.Count > 0)
                {
                    using (Brush startBrush = new SolidBrush(Color.Green))
                    using (Brush endBrush = new SolidBrush(Color.Red))
                    {
                        PixelNode start = path.Nodes[0];
                        PixelNode end = path.Nodes[path.Nodes.Count - 1];
                        g.FillEllipse(startBrush, start.X - 3, start.Y - 3, 6, 6);
                        g.FillEllipse(endBrush, end.X - 3, end.Y - 3, 6, 6);
                    }
                }
            }
            return output;
        }
    }
}
