using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.Processing
{
        internal class FloodFiller
        {
            // Applies flood fill
            public Bitmap ApplyFloodFill(Bitmap input, Point seed, Color fillColour)
            {
                Bitmap output = new Bitmap(input);
                Color targetColour = output.GetPixel(seed.X, seed.Y);

                if (targetColour.ToArgb() == fillColour.ToArgb())
                    return output;

                Queue<Point> pixels = new Queue<Point>();
                pixels.Enqueue(seed);

                while (pixels.Count > 0)
                {
                    Point p = pixels.Dequeue();

                    if (!IsWithinBounds(output, p))
                        continue;

                    if (!ShouldFillPixel(output, p, targetColour))
                        continue;

                    output.SetPixel(p.X, p.Y, fillColour);

                    EnqueueNeighbours(pixels, p);
                }

                return output;
            }

            // Checks if the point is inside the image boundary.
            private bool IsWithinBounds(Bitmap image, Point p)
            {
                return p.X >= 0 && p.X < image.Width && p.Y >= 0 && p.Y < image.Height;
            }

            // Determines if the pixel at the point should be filled.
            private bool ShouldFillPixel(Bitmap image, Point p, Color targetColour)
            {
                return image.GetPixel(p.X, p.Y).ToArgb() == targetColour.ToArgb();
            }

            // Adds the 8 connected neighbours of the current point to the queue.
            private void EnqueueNeighbours(Queue<Point> queue, Point current)
            {
                queue.Enqueue(new Point(current.X - 1, current.Y));
                queue.Enqueue(new Point(current.X + 1, current.Y));
                queue.Enqueue(new Point(current.X, current.Y - 1));
                queue.Enqueue(new Point(current.X, current.Y + 1));
                queue.Enqueue(new Point(current.X - 1, current.Y - 1));
                queue.Enqueue(new Point(current.X + 1, current.Y - 1));
                queue.Enqueue(new Point(current.X - 1, current.Y + 1));
                queue.Enqueue(new Point(current.X + 1, current.Y + 1));
            }
        }
   
}
