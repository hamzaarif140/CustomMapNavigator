using Microsoft.VisualBasic.ApplicationServices;
using NEA.Datatypes;
using NEA.Interfaces;
using NEA.Pathfinding;
using NEA.Processing;
using NEA.Utility;
using NEA.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NEA.Settings;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace NEA
{
    public partial class MainForm : Form
    {
        private ImageController imageController;       // Bridges UI to image loading and basic processing.
        private Logger logger;                           // Logs events.
        private ImageProcessingManager processingManager; // Manages the image processing

        // Pathfinding and graph-related variables
        private GridGraph roadGraph;
        private List<PixelNode> roadNodes;
        private PixelNode startNode;
        private PixelNode endNode;
        private Point floodFillSeed;

        // Booleans for  Mouse-click selections.
        private bool isSelectingStart = false;
        private bool isSelectingEnd = false;
        private bool isSelectingFloodFillSeed = false;

        // Define processing stages.
        private enum ProcessingState
        {
            None,            // No processing done.
            Greyscale,       // Converted to greyscale.
            GaussianBlur,    // Gaussian blur applied.
            GradientMagnitude, // Gradient magnitude calculated.
            GradientOrientation, // Gradient orientation calculated.
            NonMaximumSuppression, // Non-maximum suppression applied.
            DoubleThresholding,    // Double thresholding applied.
            EdgeTracking,          // Edge tracking by hysteresis applied.
            FloodFill,             // Flood fill applied.
            Final                  // All processing complete.
        }
        private ProcessingState currentState = ProcessingState.None;

        // For pathfinding.
        private DijkstraPathfinder dijkstraPathFinder = new DijkstraPathfinder();
        private RoutePlanner routePlanner;

        public MainForm()
        {
            InitializeComponent();
            imageController = new ImageController();
            logger = new Logger(logTextBox);
            logger.LogMessage("Application initialised.");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        // Upload button click loads the image, displays it, and creates the processing manager.
        private void uploadButton_Click(object sender, EventArgs e)
        {
            if (imageController.LoadImage())
            {
                Bitmap image = new Bitmap(imageController.GetImage());
                mapDisplay.Image = image;
                processingManager = new ImageProcessingManager(image, logger);
                currentState = ProcessingState.None;
                logger.LogMessage("Image loaded.");
                MessageBox.Show("Image loaded successfully!", "Success");
            }
            else
            {
                MessageBox.Show("Failed to load image.", "Error");
                logger.LogMessage("Failed to load image.");
            }
        }

        // Process button starts by converting the image to greyscale using the processing manager.
        private void processImageButton_Click(object sender, EventArgs e)
        {
            if (processingManager == null)
            {
                MessageBox.Show("Please upload an image first.", "Error");
                return;
            }
            processingManager.ConvertToGreyscale();
            mapDisplay.Image = processingManager.GetCurrentImage();
            currentState = ProcessingState.Greyscale;
            MessageBox.Show("Image processed to greyscale. Click Next to apply Gaussian blur.");
        }

        // "Next" button to advance processing stages.
        private void nextButton_Click(object sender, EventArgs e)
        {
            if (processingManager == null)
            {
                MessageBox.Show("Please upload and process an image first.", "Error");
                return;
            }

            switch (currentState)
            {
                case ProcessingState.Greyscale:
                    processingManager.ApplyGaussianBlur();
                    mapDisplay.Image = processingManager.GetCurrentImage();
                    currentState = ProcessingState.GaussianBlur;
                    MessageBox.Show("Gaussian blur applied. Click Next to calculate gradient magnitude.", "Info");
                    break;
                case ProcessingState.GaussianBlur:
                    processingManager.CalculateGradientMagnitude();
                    mapDisplay.Image = processingManager.GetCurrentImage();
                    currentState = ProcessingState.GradientMagnitude;
                    MessageBox.Show("Gradient magnitude calculated. Click Next to calculate gradient orientation.", "Info");
                    break;
                case ProcessingState.GradientMagnitude:
                    processingManager.CalculateGradientOrientation();
                    mapDisplay.Image = processingManager.GradientOrientationImage;
                    currentState = ProcessingState.GradientOrientation;
                    MessageBox.Show("Gradient orientation calculated. Click Next to apply non-maximum suppression.", "Info");
                    break;
                case ProcessingState.GradientOrientation:
                    processingManager.ApplyNonMaximumSuppression();
                    mapDisplay.Image = processingManager.GetCurrentImage();
                    currentState = ProcessingState.NonMaximumSuppression;
                    MessageBox.Show("Non-maximum suppression applied. Click Next to apply double thresholding.", "Info");
                    break;
                case ProcessingState.NonMaximumSuppression:
                    processingManager.ApplyDoubleThreshold(50, 100);
                    mapDisplay.Image = processingManager.GetCurrentImage();
                    currentState = ProcessingState.DoubleThresholding;
                    MessageBox.Show("Double thresholding applied. Click Next to apply edge tracking.", "Info");
                    break;
                case ProcessingState.DoubleThresholding:
                    processingManager.ApplyEdgeTrackingHysteresis();
                    mapDisplay.Image = processingManager.GetCurrentImage();
                    currentState = ProcessingState.EdgeTracking;
                    MessageBox.Show("Edge tracking applied. Now please set the flood fill seed point and then click Next to apply flood fill.", "Info");
                    break;
                case ProcessingState.EdgeTracking:
                    if (floodFillSeed.IsEmpty)
                    {
                        MessageBox.Show("Please set the flood fill seed point first by clicking on the image. For the flood fill to work correctly, the point must be on a road", "Info");
                        return;
                    }
                    processingManager.ApplyFloodFill(floodFillSeed, Color.White);
                    mapDisplay.Image = processingManager.GetCurrentImage();
                   // Build the road graph from the processed image.
                    roadGraph = processingManager.BuildRoadGraph(128);
                    currentState = ProcessingState.Final;
                    MessageBox.Show("Flood fill applied and road graph extracted. Processing complete.", "Info");
                    break;
                case ProcessingState.Final:
                    MessageBox.Show("Processing has already been completed. You may now choose a start and end point then click calculate route", "Info");
                    break;
                default:
                    MessageBox.Show("Please start processing by clicking 'Process Image'.", "Info");
                    break;
            }
        }

        // Calculate Route button computes and visualises the path using the processed image.
        private void calculateRouteButton_Click(object sender, EventArgs e)
        {
            if (startNode == null || endNode == null)
            {
                MessageBox.Show("Please set both a start and an end point first.", "Error");
                return;
            }
            if (roadGraph == null)
            {
                MessageBox.Show("A Graph has not been extracted yet.", "Error");
                return;
            }

            // Pick the algorithm implementation based on the setting:
            IPathFinder pathfinder;
            switch (Settings.SelectedPathfindingAlgorithm)
            {
                case PathfindingAlgorithm.AStar:
                    pathfinder = new AStarPathFinder();
                    break;
                case PathfindingAlgorithm.Dijkstra:
                default:
                    pathfinder = new DijkstraPathfinder();
                    break;
            }

            // Use the chosen pathfinder
            routePlanner = new RoutePlanner(roadGraph, pathfinder);

           
            var route = routePlanner.PlanRoute(startNode, endNode);
            var visualisedRoute = routePlanner.VisualiseRoute(route, processingManager.GetCurrentImage());
            mapDisplay.Image = visualisedRoute;

            MessageBox.Show("Route calculated and visualised.", "Success");
            logger.LogMessage("Route calculated using "
                + Settings.SelectedPathfindingAlgorithm
                + " and visualised.");
        }

        // Button handler to set the start point via a map click.
        private void setStartPointButton_Click(object sender, EventArgs e)
        {
            isSelectingStart = true;
            isSelectingEnd = false;
            isSelectingFloodFillSeed = false;
            if (roadGraph == null)
            {
                MessageBox.Show("Please finish processing the image first", "Error");
                return;
            }
            MessageBox.Show("Please click on the map to set the start point.", "Info");
            logger.LogMessage("User prompted to set start point.");
        }

        // Button handler to set the end point via a map click.
        private void setEndPointButton_Click(object sender, EventArgs e)
        {
            isSelectingStart = false;
            isSelectingEnd = true;
            isSelectingFloodFillSeed = false;
            if (roadGraph == null)
            {
                MessageBox.Show("Please finish processing the image first", "Error");
                return;
            }

                MessageBox.Show("Please click on the map to set the end point.", "Info");
            logger.LogMessage("User prompted to set end point.");
        }

        // Button handler to set the flood fill seed via a map click.
        private void setFloodFillSeedButton_Click(object sender, EventArgs e)
        {
            isSelectingStart = false;
            isSelectingEnd = false;
            isSelectingFloodFillSeed = true;
            MessageBox.Show("Please click on the map to set the flood fill seed.", "Info");
            logger.LogMessage("User prompted to set flood fill seed.");
        }

        // Mouse click event handler for the map display.
        private void mapDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            if (mapDisplay.Image == null)
                return;

            Image img = mapDisplay.Image;
            float ratioWidth = (float)mapDisplay.ClientSize.Width / img.Width;
            float ratioHeight = (float)mapDisplay.ClientSize.Height / img.Height;
            float scale = Math.Min(ratioWidth, ratioHeight);
            int displayWidth = (int)(img.Width * scale);
            int displayHeight = (int)(img.Height * scale);
            int offsetX = (mapDisplay.ClientSize.Width - displayWidth) / 2;
            int offsetY = (mapDisplay.ClientSize.Height - displayHeight) / 2;

            if (e.X < offsetX || e.X > offsetX + displayWidth || e.Y < offsetY || e.Y > offsetY + displayHeight)
                return;
            int imageX = (int)((e.X - offsetX) / scale);
            int imageY = (int)((e.Y - offsetY) / scale);

            if (isSelectingFloodFillSeed)
            {
                floodFillSeed = new Point(imageX, imageY);
                MessageBox.Show($"Flood fill seed set at ({imageX}, {imageY}).", "Info");
                logger.LogMessage($"Flood fill seed set at ({imageX}, {imageY}).");
                isSelectingFloodFillSeed = false;
            }
            else if ((isSelectingStart || isSelectingEnd) && roadGraph != null)
            {
                PixelNode nearest = MapPointToNearestNode(new Point(imageX, imageY));
                if (isSelectingStart)
                {
                    startNode = nearest;
                    MessageBox.Show($"Start point set at node {startNode}.", "Info");
                    logger.LogMessage($"Start point set at node {startNode} via click at ({imageX}, {imageY}).");
                    isSelectingStart = false;
                }
                else if (isSelectingEnd)
                {
                    endNode = nearest;
                    MessageBox.Show($"End point set at node {endNode}.", "Info");
                    logger.LogMessage($"End point set at node {endNode} via click at ({imageX}, {imageY}).");
                    isSelectingEnd = false;
                }
            }
        }


        // Maps an image coordinate to the nearest PixelNode in the grid graph.
        private PixelNode MapPointToNearestNode(Point p)
        {
            if (roadGraph == null)
                return null;
            PixelNode closest = null;
            double minDist = double.PositiveInfinity;
            foreach (var kvp in roadGraph.Nodes)
            {
                PixelNode node = kvp.Value;
                double dist = Math.Sqrt(Math.Pow(node.X - p.X, 2) + Math.Pow(node.Y - p.Y, 2));
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = node;
                }
            }
            return closest;
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
            logger.LogMessage(
                $"Settings applied: Pathfinding algorithm set to {Settings.SelectedPathfindingAlgorithm}");
        }
    private void saveButton_Click(object sender, EventArgs e)
        {
            Image imageToSave;
            if (mapDisplay.Image != null)
            {
                imageToSave = mapDisplay.Image;
            }
            else if (processingManager != null)
            {
                imageToSave = processingManager.GetCurrentImage();
            }
            else
            {
                imageToSave = null;
            }

            if (imageToSave == null)
            {
                MessageBox.Show("There is no image available to save.", "Error");
                logger.LogMessage("Save failed: no image available.");
                return;
            }

            // Open a save file dialog to ask the user where to save the image
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save Image As...";
                saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                saveFileDialog.FileName = "map.png"; // Default file name

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Decide which image format to use based on the selected file extension
                        string fileExtension = System.IO.Path.GetExtension(saveFileDialog.FileName)?.ToLower();
                        ImageFormat selectedImageFormat = ImageFormat.Png; // Default to PNG

                        if (fileExtension == ".jpg" || fileExtension == ".jpeg")
                        {
                            selectedImageFormat = ImageFormat.Jpeg;
                        }
                        else if (fileExtension == ".bmp")
                        {
                            selectedImageFormat = ImageFormat.Bmp;
                        }

                        // Save the image using the selected format
                        using (var bitmapToSave = new Bitmap(imageToSave))
                        {
                            bitmapToSave.Save(saveFileDialog.FileName, selectedImageFormat);
                        }

                        MessageBox.Show("Image saved successfully!", "Success");
                        logger.LogMessage($"Image saved at '{saveFileDialog.FileName}'.");
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("An error occurred while saving the image.", "Error");
                        logger.LogMessage($"Error saving image: {exception.Message}");
                    }
                }
            }
        }


        private void resetButton_Click(object sender, EventArgs e)
        {
            // Clear the displayed map
            mapDisplay.Image = null;

            //Reset all processing state
            processingManager = null;
            roadGraph = null;
            roadNodes = null;
            startNode = null;
            endNode = null;
            floodFillSeed = Point.Empty;
            isSelectingStart = false;
            isSelectingEnd = false;
            isSelectingFloodFillSeed = false;
            currentState = ProcessingState.None;


            // Clear the log
            logTextBox.Clear();
            // logger internal list is made empty
            logger = new Logger(logTextBox);
            logger.LogMessage("Application reset. Please upload a new map to begin.");
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
