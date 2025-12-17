using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.Processing
{
        // This class applies double thresholding to a greyscale image.
        // Pixels above the high threshold are marked as strong edges (white),
        // pixels below the low threshold are marked as non-edges (black),
        // and pixels in between are marked as weak edges (grey).
        internal class DoubleThresholder
        {
            private int lowThreshold;
            private int highThreshold;

            // Constructor sets the threshold values.
            // These define the ranges for strong, weak, and non-edges.
            public DoubleThresholder(int lowThreshold, int highThreshold)
            {
                this.lowThreshold = lowThreshold;
                this.highThreshold = highThreshold;
            }

           
            public Bitmap ApplyDoubleThreshold(Bitmap input)
            {
               
                if (input == null)
                    throw new ArgumentNullException(nameof(input));

                int width = input.Width;
                int height = input.Height;

                // Create a new image to store the thresholded result.
                Bitmap output = new Bitmap(width, height);

          
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        
                        Color current = input.GetPixel(x, y);
                        int intensity = current.R;

                        // Determine if the pixel should be a strong edge.
                        if (intensity >= highThreshold)
                        {
                            // Set to white (strong edge).
                            output.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                        }
                        // If the pixel is below the low threshold, it's not part of an edge.
                        else if (intensity < lowThreshold)
                        {
                            // Set to black (non-edge).
                            output.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                        }
                        else
                        {
                            // Otherwise, it's a weak edge. Set to grey.
                            output.SetPixel(x, y, Color.FromArgb(128, 128, 128));
                        }
                    }
                }

              
                return output;
            }
        }
   
}
