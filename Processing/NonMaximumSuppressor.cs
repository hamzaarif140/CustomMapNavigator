using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA
{

        internal class NonMaximumSuppressor
        {
            // Applies non-maximum suppression to thin the edges
            public Bitmap ApplyNonMaximumSuppression(Bitmap gradientMagnitude, Bitmap gradientOrientation)
            {
                int width = gradientMagnitude.Width;
                int height = gradientMagnitude.Height;
                Bitmap output = new Bitmap(width, height);

              
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        // Get the magnitude at the current pixel.
                        int currentMagnitude = gradientMagnitude.GetPixel(x, y).R;

                        // Convert orientation (stored as scaled value 0–255) to angle in degrees.
                        double angle = ConvertOrientationToAngle(gradientOrientation.GetPixel(x, y).R);

                        // Classify angle into one of four directions: 0, 45, 90, or 135.
                        double direction = ClassifyDirection(angle);

                        // Get the two neighbouring pixels in the gradient direction.
                        Point neighbour1 = GetFirstNeighbour(x, y, direction);
                        Point neighbour2 = GetSecondNeighbour(x, y, direction);

                        neighbour1 = ClampToBounds(neighbour1, width, height);
                        neighbour2 = ClampToBounds(neighbour2, width, height);

                        // Get magnitudes of neighbouring pixels.
                        int mag1 = gradientMagnitude.GetPixel(neighbour1.X, neighbour1.Y).R;
                        int mag2 = gradientMagnitude.GetPixel(neighbour2.X, neighbour2.Y).R;

                        // Retain pixel only if it's a local maximum.
                        if (currentMagnitude >= mag1 && currentMagnitude >= mag2)
                        {
                            output.SetPixel(x, y, Color.FromArgb(currentMagnitude, currentMagnitude, currentMagnitude));
                        }
                        else
                        {
                            output.SetPixel(x, y, Color.Black);
                        }
                    }
                }

                return output;
            }

            // Converts scaled orientation (0–255) into degrees (0–180).
            private double ConvertOrientationToAngle(int scaled)
            {
                double angle = (scaled / 255.0) * 180.0;
                if (angle < 0)
                    angle += 180;
                return angle;
            }

            // Classifies the orientation angle into one of four principal directions.
            private double ClassifyDirection(double angle)
            {
                if ((angle >= 0 && angle < 22.5) || (angle >= 157.5 && angle <= 180))
                    return 0;
                else if (angle >= 22.5 && angle < 67.5)
                    return 45;
                else if (angle >= 67.5 && angle < 112.5)
                    return 90;
                else
                    return 135;
            }

            // Returns the first neighbour in the gradient direction.
            private Point GetFirstNeighbour(int x, int y, double direction)
            {
                if (direction == 0)
                    return new Point(x - 1, y);
                else if (direction == 45)
                    return new Point(x - 1, y - 1);
                else if (direction == 90)
                    return new Point(x, y - 1);
                else // direction == 135
                    return new Point(x + 1, y - 1);
            }

            // Returns the second neighbour in the gradient direction (opposite of first).
            private Point GetSecondNeighbour(int x, int y, double direction)
            {
                if (direction == 0)
                    return new Point(x + 1, y);
                else if (direction == 45)
                    return new Point(x + 1, y + 1);
                else if (direction == 90)
                    return new Point(x, y + 1);
                else // direction == 135
                    return new Point(x - 1, y + 1);
            }

            // Ensures a point lies within image bounds.
            private Point ClampToBounds(Point p, int width, int height)
            {
                int clampedX = p.X;
                int clampedY = p.Y;

                if (clampedX < 0)
                    clampedX = 0;
                else if (clampedX >= width)
                    clampedX = width - 1;

                if (clampedY < 0)
                    clampedY = 0;
                else if (clampedY >= height)
                    clampedY = height - 1;

                return new Point(clampedX, clampedY);
            }
        }

}
