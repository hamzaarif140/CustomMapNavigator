using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Datatypes;

namespace NEA.Processing
{
        internal class GaussianBlurApplier
        {
            // Public method to apply Gaussian blur to an image using the provided kernel.
            public Bitmap ApplyGaussianBlur(Bitmap input, Kernel kernel)
            {
                int width = input.Width;
                int height = input.Height;
                Bitmap output = new Bitmap(width, height);

                // Loop over each pixel in the input image.
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        // Calculate the blurred intensity for the current pixel.
                        int blurredIntensity = CalculateBlurredIntensity(input, x, y, kernel);

                        // Create a greyscale colour using the blurred intensity.
                        Color newColour = Color.FromArgb(blurredIntensity, blurredIntensity, blurredIntensity);

                        // Set the new colour in the output image.
                        output.SetPixel(x, y, newColour);
                    }
                }

                return output;
            }

            // Calculates the weighted average intensity for a pixel at (x, y)  by applying the Gaussian kernel.
            private int CalculateBlurredIntensity(Bitmap input, int x, int y, Kernel kernel)
            {
                double sum = 0;

                int kernelRows = kernel.GetRows();
                int kernelCols = kernel.GetColumns();
                int centreY = kernelRows / 2;
                int centreX = kernelCols / 2;

                // Loop over each element of the kernel.
                for (int dy = -centreY; dy <= centreY; dy++)
                {
                    for (int dx = -centreX; dx <= centreX; dx++)
                    {
                        // Calculate coordinates of the neighbouring pixel.
                        int neighbourX = x + dx;
                        int neighbourY = y + dy;

                        // Apply replication padding at the image boundaries.
                        neighbourX = Clamp(neighbourX, 0, input.Width - 1);
                        neighbourY = Clamp(neighbourY, 0, input.Height - 1);

                        // Get intensity value (since it's greyscale, use R component).
                        Color neighbourColour = input.GetPixel(neighbourX, neighbourY);
                        double intensity = neighbourColour.R;

                        // Get the corresponding weight from the Gaussian kernel.
                        double weight = kernel.GetValue(dy + centreY, dx + centreX);

                        // Add the weighted intensity to the sum.
                        sum += intensity * weight;
                    }
                }

                // Round the result and clamp to [0, 255] to ensure valid pixel intensity.
                int blurredIntensity = (int)Math.Round(sum);
                if (blurredIntensity < 0)
                    blurredIntensity = 0;
                else if (blurredIntensity > 255)
                    blurredIntensity = 255;

                return blurredIntensity;
            }

            // Ensures a value stays within the min and max bounds.
            private int Clamp(int value, int min, int max)
            {
                if (value < min)
                    return min;
                else if (value > max)
                    return max;
                return value;
            }
        }
}
