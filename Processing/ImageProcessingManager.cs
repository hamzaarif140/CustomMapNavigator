using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Datatypes;
using NEA.Processing;
using NEA.Utility;

namespace NEA.Processing
{
    // This class manages the entire image processing in sequence
    internal class ImageProcessingManager
    {
        private Bitmap originalImage;            
        private Bitmap processedImage;            
        private Kernel gaussianKernel;          
        private Logger logger;                   

        public Bitmap GradientMagnitudeImage { get; private set; }    
        public Bitmap GradientOrientationImage { get; private set; } 

       
        public ImageProcessingManager(Bitmap image, Logger logger)
        {
            // Ensure that the input image is not null.
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image), "Image cannot be null.");
            }

            // Ensure that the logger is not null.
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
            }

            this.originalImage = image;
            this.logger = logger;

         
            processedImage = new Bitmap(originalImage);
        }

        // Returns the image currently being worked on.
        public Bitmap GetCurrentImage()
        {
            return processedImage;
        }

        // Converts the current image to greyscale using the GreyscaleConverter class.
        public void ConvertToGreyscale()
        {
            GreyscaleConverter converter = new GreyscaleConverter();
            processedImage = converter.ConvertToGreyscale(processedImage);
            logger.LogMessage("Converted image to greyscale.");
        }

        // Applies Gaussian blur using a kernel of given size and sigma.
        public void ApplyGaussianBlur(int kernelSize = 3, double sigma = 1.0)
        {
            gaussianKernel = Kernel.CreateGaussianKernel(kernelSize, sigma, logger);
            GaussianBlurApplier blur = new GaussianBlurApplier();
            processedImage = blur.ApplyGaussianBlur(processedImage, gaussianKernel);
            logger.LogMessage("Applied Gaussian blur.");
        }

        // Calculates gradient magnitude using Sobel operators.
        public void CalculateGradientMagnitude()
        {
            GradientMagnitudeCalculator magnitudeCalc = new GradientMagnitudeCalculator();
            GradientMagnitudeImage = magnitudeCalc.CalculateGradientMagnitude(processedImage);
            processedImage = GradientMagnitudeImage; // Update the processed image
            logger.LogMessage("Calculated gradient magnitude.");
        }

        // Calculates gradient orientation using arctangent of Sobel results.
        public void CalculateGradientOrientation()
        {
            GradientOrientationCalculator orientationCalc = new GradientOrientationCalculator();
            GradientOrientationImage = orientationCalc.CalculateGradientOrientation(processedImage);
            logger.LogMessage("Calculated gradient orientation.");
        }

        // Applies non-maximum suppression using gradient magnitude and orientation.
        public void ApplyNonMaximumSuppression()
        {
            NonMaximumSuppressor suppressor = new NonMaximumSuppressor();
            processedImage = suppressor.ApplyNonMaximumSuppression(GradientMagnitudeImage, GradientOrientationImage);
            logger.LogMessage("Applied non-maximum suppression.");
        }

        // Applies double thresholding to classify edge strength.
        public void ApplyDoubleThreshold(int lowThreshold, int highThreshold)
        {
            DoubleThresholder threshold = new DoubleThresholder(lowThreshold, highThreshold);
            processedImage = threshold.ApplyDoubleThreshold(processedImage);
            logger.LogMessage($"Applied double thresholding (Low: {lowThreshold}, High: {highThreshold}).");
        }

        // Applies edge tracking by hysteresis to finalize strong edges.
        public void ApplyEdgeTrackingHysteresis()
        {
            EdgeTrackingHysteresisApplier tracker = new EdgeTrackingHysteresisApplier();
            processedImage = tracker.ApplyEdgeTrackingHysteresis(processedImage);
            logger.LogMessage("Applied edge tracking by hysteresis.");
        }

        // Applies flood fill from a user-selected seed point to isolate a continuous region.
        public void ApplyFloodFill(Point seed, Color fillColour)
        {
            FloodFiller filler = new FloodFiller();
            processedImage = filler.ApplyFloodFill(processedImage, seed, fillColour);
            logger.LogMessage($"Applied flood fill at seed {seed}.");
        }

        // Returns the final processed image after all steps.
        public Bitmap GetProcessedImage()
        {
            return processedImage;
        }

        // Builds a grid graph (road graph) from the current processed image.
        // It extracts a binary road mask using the RoadMaskExtractor and then extracts nodes using RoadGraphExtractor.
        public GridGraph BuildRoadGraph(int threshold)
        {
            RoadMaskExtractor maskExtractor = new RoadMaskExtractor(threshold);
            Bitmap roadMask = maskExtractor.ExtractMask(GetCurrentImage());
            logger.LogMessage("Road mask extracted from processed image.");

            RoadGraphExtractor nodeExtractor = new RoadGraphExtractor();
            var nodes = nodeExtractor.ExtractNodes(roadMask);
            logger.LogMessage($"Extracted {nodes.Count} nodes from the road mask.");

            GridGraph graph = new GridGraph();
            graph.BuildGraph(nodes);
            logger.LogMessage("Grid graph built from extracted nodes.");
            return graph;
        }
    }
}
