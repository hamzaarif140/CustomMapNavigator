using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA
{
    internal class RoadMaskExtractor
    {
        private int threshold;

        // Constructor that sets the threshold value.
        // Pixels with intensity equal to or above this threshold are considered part of the road.
        public RoadMaskExtractor(int threshold)
        {
            this.threshold = threshold;
        }

        // Extracts a binary road mask from the input image.
        // Road pixels are set to White; non-road pixels are set to Black.
        public Bitmap ExtractMask(Bitmap input)
        {
            if (input == null)
                throw new ArgumentNullException("input", "Input image cannot be null.");

            int width = input.Width;
            int height = input.Height;
            Bitmap mask = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color colour = input.GetPixel(x, y);
                    int intensity = colour.R; // Assuming input is greyscale
                    if (intensity >= threshold)
                    {
                        mask.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        mask.SetPixel(x, y, Color.Black);
                    }
                }
            }
            return mask;
        }
    }
}