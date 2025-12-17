using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Datatypes;
using NEA.Processing;

namespace NEA.WindowsForms
{
    public class ImageController
    {
        private Bitmap currentImage;           // Holds the currently loaded image

        //  Method to load an image from the user's file system 
        public bool LoadImage()
        {
            // Opens a file dialog for the user to select an image
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a Map Image";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"; // Allow only image files

            // If the user selects an image and clicks "Open"
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                // Load the selected image into the currentImage variable
                currentImage = new Bitmap(filePath);

              
                return true; 
            }

            return false; // No image was loaded
        }

       
        public Image GetImage()
        {
            return currentImage;
        }

     
       
    }
}
