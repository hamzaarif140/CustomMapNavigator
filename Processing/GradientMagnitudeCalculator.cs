using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Datatypes;

namespace NEA.Processing
{
        internal class GradientMagnitudeCalculator
        {
            public Bitmap CalculateGradientMagnitude(Bitmap input)
            {
                int width = input.Width;
                int height = input.Height;
                Bitmap output = new Bitmap(width, height);

                Kernel sobelX = CreateSobelXKernel();
                Kernel sobelY = CreateSobelYKernel();

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        // Apply Sobel kernels to get the gradient components.
                        double gx = ApplyConvolution(input, sobelX, x, y);
                        double gy = ApplyConvolution(input, sobelY, x, y);

                        // Compute the magnitude of the gradient vector.
                        double magnitude = Math.Sqrt(gx * gx + gy * gy);

                        // Convert to integer in range [0, 255].
                        int magInt = (int)Math.Round(magnitude);
                        if (magInt < 0) magInt = 0;
                        if (magInt > 255) magInt = 255;

                        Color outputColour = Color.FromArgb(magInt, magInt, magInt);
                        output.SetPixel(x, y, outputColour);
                    }
                }

                return output;
            }

            // Applies convolution of the kernel at a specific pixel (x, y)
            private double ApplyConvolution(Bitmap image, Kernel kernel, int x, int y)
            {
                int kernelSize = kernel.GetRows();
                int kernelCentre = kernelSize / 2;
                double sum = 0;

                for (int i = -kernelCentre; i <= kernelCentre; i++)
                {
                    for (int j = -kernelCentre; j <= kernelCentre; j++)
                    {
                        int pixelX = x + j;
                        int pixelY = y + i;

                        // Replication padding: clamp to image bounds
                        if (pixelX < 0) pixelX = 0;
                        if (pixelX >= image.Width) pixelX = image.Width - 1;
                        if (pixelY < 0) pixelY = 0;
                        if (pixelY >= image.Height) pixelY = image.Height - 1;

                        Color pixel = image.GetPixel(pixelX, pixelY);
                        double intensity = pixel.R; // Greyscale

                        double weight = kernel.GetValue(i + kernelCentre, j + kernelCentre);
                        sum += intensity * weight;
                    }
                }

                return sum;
            }

            // Creates the Sobel kernel for horizontal gradient (X direction)
            private Kernel CreateSobelXKernel()
            {
                double[,] sobelX = new double[3, 3]
                {
                { -1, 0, 1 },
                { -2, 0, 2 },
                { -1, 0, 1 }
                };

                return new Kernel(sobelX);
            }

            // Creates the Sobel kernel for vertical gradient (Y direction)
            private Kernel CreateSobelYKernel()
            {
                double[,] sobelY = new double[3, 3]
                {
                { -1, -2, -1 },
                {  0,  0,  0 },
                {  1,  2,  1 }
                };

                return new Kernel(sobelY);
            }
        }
}
