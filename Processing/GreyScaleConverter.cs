using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA
{

    internal class GreyscaleConverter
    {
        // The constants are based off how the human eye perceives colour.
        private const double RedWeight = 0.30;
        private const double GreenWeight = 0.59;
        private const double BlueWeight = 0.11;
        // Converts the input image to greyscale and returns the new image.
        public Bitmap ConvertToGreyscale(Bitmap input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input", "Input image cannot be null.");
            }

            // Create a new Bitmap with the same dimensions as the original image.
            Bitmap greyImage = new Bitmap(input.Width, input.Height);

            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    // Gets the current pixel colour.
                    // The structure 'Color' stores an Alpha (how transparent the pixel is) Red, Green and Blue value. Each between 0 and 255
                    Color originalColour = input.GetPixel(x, y);

                    
                    int greyValue = (int)((originalColour.R * RedWeight) +
                      (originalColour.G * GreenWeight) +
                      (originalColour.B * BlueWeight));

                    // ensures greyscale value is between 0 and 255.
                    if (greyValue < 0)
                    {
                        greyValue = 0;
                    }
                    else if (greyValue > 255)
                    {
                        greyValue = 255;
                    }

                    // Create a new colour using the greyscale value for red, green, and blue.
                    Color greyColour = Color.FromArgb(greyValue, greyValue, greyValue);

                    greyImage.SetPixel(x, y, greyColour);
                }
            }

           
            return greyImage;
        }
    }
}
