using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Datatypes;

namespace NEA.Processing
{
    internal class GradientOrientationCalculator
    {
        
        public Bitmap CalculateGradientOrientation(Bitmap input)
        {
            int width = input.Width;
            int height = input.Height;
            Bitmap output = new Bitmap(width, height);

            // Get Sobel kernels for X and Y directions
            Kernel sobelX = GetSobelXKernel();
            Kernel sobelY = GetSobelYKernel();

            // For each pixel, calculate gradient orientation
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Compute the horizontal and vertical gradients
                    double gx = ApplyKernelAtPixel(input, sobelX, x, y);
                    double gy = ApplyKernelAtPixel(input, sobelY, x, y);

                    // Calculate the orientation in radians
                    double orientation = Math.Atan2(gy, gx);

                    // Convert to degrees and normalise to 0–180 range
                    double degrees = orientation * (180.0 / Math.PI);
                    if (degrees < 0)
                        degrees += 180;

                    // Scale orientation to fit in 0–255 range for display
                    int scaled = (int)Math.Round((degrees / 180.0) * 255);
                    if (scaled < 0) scaled = 0;
                    if (scaled > 255) scaled = 255;

                    // Set the pixel in the output image
                    Color orientationColor = Color.FromArgb(scaled, scaled, scaled);
                    output.SetPixel(x, y, orientationColor);
                }
            }

            return output;
        }

        // Returns the Sobel kernel for detecting horizontal gradients
        private Kernel GetSobelXKernel()
        {
            double[,] kernelData = new double[,]
            {
                { -1, 0, 1 },
                { -2, 0, 2 },
                { -1, 0, 1 }
            };
            return new Kernel(kernelData);
        }

        // Returns the Sobel kernel for detecting vertical gradients
        private Kernel GetSobelYKernel()
        {
            double[,] kernelData = new double[,]
            {
                { -1, -2, -1 },
                {  0,  0,  0 },
                {  1,  2,  1 }
            };
            return new Kernel(kernelData);
        }

        // Applies a kernel at a specific pixel position in the image
        private double ApplyKernelAtPixel(Bitmap image, Kernel kernel, int x, int y)
        {
            int kernelSize = kernel.GetRows(); // Assuming square kernel
            int centre = kernelSize / 2;
            double sum = 0;

            // Loop through the kernel
            for (int i = -centre; i <= centre; i++)
            {
                for (int j = -centre; j <= centre; j++)
                {
                    int neighbourX = x + j;
                    int neighbourY = y + i;

                    // Replication padding at borders
                    if (neighbourX < 0) neighbourX = 0;
                    if (neighbourX >= image.Width) neighbourX = image.Width - 1;
                    if (neighbourY < 0) neighbourY = 0;
                    if (neighbourY >= image.Height) neighbourY = image.Height - 1;

                    Color pixel = image.GetPixel(neighbourX, neighbourY);
                    double intensity = pixel.R;

                    double weight = kernel.GetValue(i + centre, j + centre);
                    sum += intensity * weight;
                }
            }

            return sum;
        }
    }
}
