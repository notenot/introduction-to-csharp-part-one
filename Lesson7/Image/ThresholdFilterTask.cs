using System.Linq;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
		    var width = original.GetLength(0);
		    var height = original.GetLength(1);
		    var filtered = new double[width, height];
            var thresholdValue = FindThresholdValue(original, whitePixelsFraction);

		    for (var y = 0; y < height; ++y)
		        for (var x = 0; x < width; ++x)
		            filtered[x, y] = original[x, y] >= thresholdValue ? 1.0 : 0.0;

            return filtered;
		}

        private static double FindThresholdValue(double[,] original, double whitePixelsFraction)
	    {
	        var pixelsList = original.Cast<double>().ToList();
	        pixelsList.Sort();
	        var whitePixelsCount = (int)(whitePixelsFraction * pixelsList.Count);
	        return whitePixelsCount == 0 ?
	            pixelsList.Last() + 1:
                pixelsList[pixelsList.Count - whitePixelsCount];
	    }
    }
}