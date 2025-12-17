using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.Processing
{
    internal class EdgeTrackingHysteresisApplier
    {
        public Bitmap ApplyEdgeTrackingHysteresis(Bitmap input)
        {
            int width = input.Width;
            int height = input.Height;

            Bitmap output = new Bitmap(input);

            // Promote weak edge pixels (value 128) connected to strong edge pixels (value 255).
            ExpandStrongEdges(output, width, height);

            // Remove remaining weak edge pixels that were not connected to strong ones.
            SuppressRemainingWeakEdges(output, width, height);

            return output;
        }

        // expands strong edge pixels to weak edge pixels connected in an 8-neighbourhood.
        private void ExpandStrongEdges(Bitmap image, int width, int height)
        {
            bool changed = true;

            // Keep iterating until no more weak pixels are promoted.
            while (changed)
            {
                changed = false;

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Color current = image.GetPixel(x, y);

                        // Process only weak edge pixels.
                        if (current.R == 128)
                        {
                            if (IsConnectedToStrongEdge(image, x, y, width, height))
                            {
                                // Promote to strong edge.
                                image.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                                changed = true;
                            }
                        }
                    }
                }
            }
        }

        // Checks all 8 neighbours of the given pixel to see if any is a strong edge.
        private bool IsConnectedToStrongEdge(Bitmap image, int x, int y, int width, int height)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    // Skip the centre pixel.
                    if (dx == 0 && dy == 0)
                        continue;

                    int neighbourX = x + dx;
                    int neighbourY = y + dy;

                    // Apply replication boundary handling.
                    if (neighbourX < 0) neighbourX = 0;
                    if (neighbourX >= width) neighbourX = width - 1;
                    if (neighbourY < 0) neighbourY = 0;
                    if (neighbourY >= height) neighbourY = height - 1;

                    Color neighbour = image.GetPixel(neighbourX, neighbourY);

                    // If any neighbour is a strong edge (255), return true.
                    if (neighbour.R == 255)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // Converts all remaining weak edge pixels (value 128) to black (non-edge).
        private void SuppressRemainingWeakEdges(Bitmap image, int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixel = image.GetPixel(x, y);

                    // If still a weak edge after propagation, suppress it.
                    if (pixel.R == 128)
                    {
                        image.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    }
                }
            }
        }
    }
}
