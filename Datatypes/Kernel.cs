using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Utility;


namespace NEA.Datatypes
{
    internal class Kernel : Matrix
    {
        // Constructor that initialises a kernel with given dimensions.
        public Kernel(int rows, int cols) : base(rows, cols) { }

        // Constructor that initialises a kernel from a 2D array.
        public Kernel(double[,] initialData) : base(initialData) { }

       
        public static Kernel CreateGaussianKernel(int size, double sigma, Logger logger)
        {
            logger.LogMessage("Starting Gaussian kernel creation.");

           
            ValidateKernelSize(size, logger);

            // Populate a temporary Matrix with unnormalised Gaussian values.
            Matrix tempMatrix = PopulateGaussianKernelMatrix(size, sigma, logger);

            // Normalise the Matrix so that the sum of all elements is 1.
            NormaliseKernel(tempMatrix, logger);

            // Create a new Kernel based on the normalised data.
            Kernel kernel = new Kernel(tempMatrix.ToArray());
            logger.LogMessage("Gaussian kernel created successfully.");
            return kernel;
        }

        // Validates that the kernel size is a positive odd number.
        private static void ValidateKernelSize(int size, Logger logger)
        {
            if (size % 2 == 0 || size <= 0)
                throw new ArgumentException("Kernel size must be a positive odd number.", nameof(size));
        }

        
        private static Matrix PopulateGaussianKernelMatrix(int size, double sigma, Logger logger)
        {
            logger.LogMessage("Populating Gaussian kernel data with size " + size + " and sigma " + sigma);
            Matrix matrix = new Matrix(size, size);
            int radius = size / 2;
            double sigma2 = sigma * sigma;
            double twoSigmaSquare = 2 * sigma2;
            double factor = 1.0 / (2 * Math.PI * sigma2);

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    double exponent = -((x * x + y * y) / twoSigmaSquare);
                    double value = factor * Math.Exp(exponent);
                    matrix.SetValue(x + radius, y + radius, value);
                }
            }
            logger.LogMessage("Gaussian kernel data populated.");
            return matrix;
        }

  
        private static void NormaliseKernel(Matrix matrix, Logger logger)
        {
            logger.LogMessage("Normalising kernel.");
            double sum = 0;
            int rows = matrix.GetRows();
            int cols = matrix.GetColumns();

            // Compute the total sum of all values.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sum += matrix.GetValue(i, j);
                }
            }
            if (sum == 0)
                throw new InvalidOperationException("Cannot normalise a kernel with a sum of zero.");

            // Divide each element by the total sum.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double normalisedValue = matrix.GetValue(i, j) / sum;
                    matrix.SetValue(i, j, normalisedValue);
                }
            }
            logger.LogMessage("Kernel normalised. Sum is now 1.");
        }
    }

}
