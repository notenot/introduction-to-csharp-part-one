using System;
using System.Drawing;

namespace Fractals
{
	internal static class DragonFractalTask
	{
		public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
		{
            var currentPixel = new Tuple<double, double>(1.0, 0.0);
		    var random = new Random(seed);

            for (var i = 0; i < iterationsCount; ++i)
            {
                currentPixel = random.Next(2) == 0 ?
                    PerformFirstTransformation(currentPixel) :
                    PerformSecondTransformation(currentPixel);

                pixels.SetPixel(currentPixel.Item1, currentPixel.Item2);
            }
        }

        /// <summary>
        /// Выполняет поворот на 45° и сжатие в sqrt(2) раз.
        /// </summary>
        /// <param name="pixel">Начальная точка</param>
        /// <returns>Точка, полученная в результате применения преобразования</returns>
	    private static Tuple<double, double> PerformFirstTransformation(Tuple<double, double> pixel)
	    {
	        var x = pixel.Item1;
	        var y = pixel.Item2;
	        var angle = 45 * Math.PI / 180.0;

            return new Tuple<double, double>(
                (x * Math.Cos(angle) - y * Math.Sin(angle)) / Math.Sqrt(2), 
	            (x * Math.Sin(angle) + y * Math.Cos(angle)) / Math.Sqrt(2));
        }

        /// <summary>
        /// Выполняет поворот на 135°, сжатие в sqrt(2) раз и сдвиг по X на единицу.
        /// </summary>
        /// <param name="pixel">Начальная точка</param>
        /// <returns>Точка, полученная в результате применения преобразования</returns>
        private static Tuple<double, double> PerformSecondTransformation(Tuple<double, double> pixel)
	    {
	        var x = pixel.Item1;
	        var y = pixel.Item2;
	        var angle = 135 * Math.PI / 180.0;

	        return new Tuple<double, double>(
	            (x * Math.Cos(angle) - y * Math.Sin(angle)) / Math.Sqrt(2) + 1,
	            (x * Math.Sin(angle) + y * Math.Cos(angle)) / Math.Sqrt(2));
        }
	}
}