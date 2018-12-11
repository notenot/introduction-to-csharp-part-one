using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		/* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */
		public static double[,] MedianFilter(double[,] original)
		{
		    var width = original.GetLength(0);
		    var height = original.GetLength(1);
		    var filtered = new double[width, height];

            for (var y = 0; y < height; ++y)
                for (var x = 0; x < width; ++x)
                    filtered[x, y] = ApplyFilterToPixel(original, x, y);

            return filtered;
		}

	    private static double ApplyFilterToPixel(double[,] original, int x, int y)
	    {
            var nearestPixels = new List<double>();
	        var startX = Math.Max(0, x - 1);
	        var startY = Math.Max(0, y - 1);
	        var endX = Math.Min(original.GetLength(0) - 1, x + 1);
	        var endY = Math.Min(original.GetLength(1) - 1, y + 1);

	        for (var j = startY; j < endY + 1; ++j)
                for (var i = startX; i < endX + 1; ++i)
                    nearestPixels.Add(original[i, j]);
            
            return CalculateMedian(nearestPixels);
	    }

        public static double CalculateMedian(List<double> values)
        {
            var sortedValues = values.OrderBy(x => x).ToList(); 
            var middle = sortedValues.Count / 2;
            if (sortedValues.Count % 2 == 0)
                return (sortedValues[middle - 1] + sortedValues[middle]) / 2;
            return sortedValues[middle];
        }
    }
}