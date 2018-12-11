using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        /* 
		Разберитесь, как работает нижеследующий код (называемый фильтрацией Собеля), 
		и какое отношение к нему имеют эти матрицы:
		
		     | -1 -2 -1 |           | -1  0  1 |                      | -1  0  1 |        | -1 -2 -1 |
		Sx = |  0  0  0 |      Sy = | -2  0  2 |  В pdf наоборот Sx = | -2  0  2 | и Sy = |  0  0  0 |
		     |  1  2  1 |           | -1  0  1 |                      | -1  0  1 |        |  1  2  1 |
		 
		https://ru.wikipedia.org/wiki/%D0%9E%D0%BF%D0%B5%D1%80%D0%B0%D1%82%D0%BE%D1%80_%D0%A1%D0%BE%D0%B1%D0%B5%D0%BB%D1%8F
		
		Попробуйте заменить фильтр Собеля 3x3 на фильтр Собеля 5x5 или другой аналогичный фильтр и сравните результаты. 
		Матрицу для фильтра Собеля 5x5 и другие матрицы можно посмотреть в статье SobelScharrGradients5x5.pdf в архиве с проектом.
		Там Sx и Sy названы соответственно β и γ.

		Обобщите код применения фильтра так, чтобы можно было передавать ему любые матрицы, любого нечетного размера.
		Фильтры Собеля размеров 3 и 5 должны быть частным случаем. 
		После такого обобщения менять фильтр Собеля одного размера на другой должно быть легко.
		*/

        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var sxWidth = sx.GetLength(0);
            var sxHeight = sx.GetLength(1);
            var indent = sxWidth == 1 ? 0 : (sxWidth - 1) / 2;

            var result = new double[width, height];
            for (var x = indent; x < width - indent; x++)
                for (var y = indent; y < height - indent; y++) 
                {
                    var gx = 0.0;
                    var gy = 0.0;

                    for (var i = 0; i < sxWidth; ++i)
                        for (var j = 0; j < sxHeight; ++j)
                        {
                            gx += sx[i, j] * g[x + i - indent, y + j - indent];
                            gy += sx[j, i] * g[x + i - indent, y + j - indent];
                        }

                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            return result;
        }
    }
}