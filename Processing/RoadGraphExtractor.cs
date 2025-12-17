using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Datatypes;

namespace NEA.Processing

{
    // This class iterates over the binary road mask and creates a PixelNode for every passable pixel (every white pixel).
    internal class RoadGraphExtractor
    {

        // Every white pixel (RGB all 255) is considered passable and becomes a node.
        public List<PixelNode> ExtractNodes(Bitmap roadMask)
        {
            if (roadMask == null)
                throw new ArgumentNullException("roadMask", "The road mask cannot be null.");

            List<PixelNode> nodes = new List<PixelNode>();
            int width = roadMask.Width;
            int height = roadMask.Height;

            // Iterate over every pixel in the road mask.
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixel = roadMask.GetPixel(x, y);
                    // Assume the road mask uses white (255,255,255) for road pixels.
                    if (pixel.R == 255 && pixel.G == 255 && pixel.B == 255)
                    {
                        nodes.Add(new PixelNode(x, y));
                    }
                }
            }
            return nodes;
        }
    }
}
