using System;

namespace Rectangles
{
	public static class RectanglesTask
	{
		// Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
		public static bool AreIntersected(Rectangle r1, Rectangle r2)
		{
            return !(r1.Left > r2.Right || r1.Right < r2.Left || 
		             r1.Bottom < r2.Top || r1.Top > r2.Bottom);
        }

        // Площадь пересечения прямоугольников
        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            var top = Math.Max(r1.Top, r2.Top);
            var left = Math.Max(r1.Left, r2.Left);
            var bottom = Math.Min(r1.Bottom, r2.Bottom);
            var right = Math.Min(r1.Right, r2.Right);

            return bottom > top && right > left ? // проверка на вырожденное пересечение
                (bottom - top) * (right - left) : 
                0; 
		}

		// Если один из прямоугольников целиком находится внутри другого — вернуть номер (с нуля) внутреннего.
		// Иначе вернуть -1.
		// Если прямоугольники совпадают, можно вернуть номер любого из них.
		public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
		{
            if (r1.Left >= r2.Left && r1.Left <= r2.Right &&
		        r1.Top >= r2.Top && r1.Top <= r2.Bottom &&     // первая вершина
		        r1.Right >= r2.Left && r1.Right <= r2.Right &&
		        r1.Bottom >= r2.Top && r1.Bottom <= r2.Bottom) // вторая вершина
		        return 0;

		    if (r2.Left >= r1.Left && r2.Left <= r1.Right &&
		        r2.Top >= r1.Top && r2.Top <= r1.Bottom &&     // первая вершина
		        r2.Right >= r1.Left && r2.Right <= r1.Right &&
		        r2.Bottom >= r1.Top && r2.Bottom <= r1.Bottom) // вторая вершина
                return 1;

            return -1;
		}
	}
}