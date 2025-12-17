using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NEA.Datatypes
{
    internal class Matrix
    {
        private double[,] data;
        private int rows;
        private int cols;

        // Constructor to initialise the matrix with given dimensions
        public Matrix(int rows, int cols)
        {
            if (rows <= 0 || cols <= 0)
            {
                throw new ArgumentException("Matrix dimensions must be greater than zero.");
            }

            this.rows = rows;
            this.cols = cols;
            data = new double[rows, cols];
        }

        // Constructor to initialise matrix with a 2D array
        public Matrix(double[,] initialData)
        {
            if (initialData == null)
            {
                throw new ArgumentNullException("Initial data cannot be null.");
            }

            rows = initialData.GetLength(0);
            cols = initialData.GetLength(1);
            data = (double[,])initialData.Clone(); // make a copy of the array when creating the matrix so original array is not changed.
        }

        // Property to get number of rows
        public int GetRows()
        {
            return rows;
        }

        // Property to get number of columns
        public int GetColumns()
        {
            return cols;
        }

        //  get the value at a specific position
        public double GetValue(int row, int col)
        {
            ValidateIndices(row, col);
            return data[row, col];
        }

        // set the value at a specific position
        public void SetValue(int row, int col, double value)
        {
            ValidateIndices(row, col);
            data[row, col] = value;
        }

        //  add two matrices
        public static Matrix Add(Matrix a, Matrix b)
        {
            if (a.rows != b.rows || a.cols != b.cols)
            {
                throw new InvalidOperationException("Matrices must have the same dimensions for addition.");
            }

            Matrix result = new Matrix(a.rows, a.cols);
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < a.cols; j++)
                {
                    result.SetValue(i, j, a.GetValue(i, j) + b.GetValue(i, j));
                }
            }
            return result;
        }

        // multiply two matrices
        public static Matrix Multiply(Matrix a, Matrix b)
        {
            if (a.cols != b.rows)
            {
                throw new InvalidOperationException("Number of columns in first matrix must match number of rows in second matrix.");
            }

            Matrix result = new Matrix(a.rows, b.cols);
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < b.cols; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < a.cols; k++)
                    {
                        sum += a.GetValue(i, k) * b.GetValue(k, j);
                    }
                    result.SetValue(i, j, sum);
                }
            }
            return result;
        }

        // display the matrix
        public void Display()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(data[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        // return matrix as an array
        public double[,] ToArray()
        {
            return (double[,])data.Clone();
        }

        // validate row and column indices
        private void ValidateIndices(int row, int col)
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols)
            {
                throw new IndexOutOfRangeException("Row or column index is out of bounds.");
            }
        }
    }

}




