using System;

namespace DistanceTask
{
	public static class DistanceTask
	{
		// Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
		public static double GetDistanceToSegment(
		    double ax, double ay, double bx, double by, double x, double y)
		{
		    var ab = GetDistanceBetweenTwoPoints(ax, ay, bx, by);
		    var bc = GetDistanceBetweenTwoPoints(bx, by, x, y);
            var ac = GetDistanceBetweenTwoPoints(ax, ay, x, y);

		    if (IsEqual(ac, 0) && IsEqual(bc, 0))
		        return 0;

            if (IsEqual(ab, 0))
		        return ac;

            if (AreFormObtuseAngle(ax, ay, bx, by, x, y))
		        return bc; 

		    if (AreFormObtuseAngle(bx, by, ax, ay, x, y))
		        return ac;

            // Применяем формулу Герона 
            var p = (ab + bc + ac) / 2;
		    return IsEqual(p, ab) || IsEqual(p, bc) || IsEqual(p, ac) ? 
		        0 :
		        2 * Math.Sqrt(p * (p - ab) * (p - bc) * (p - ac)) / ab;
		}
        
	    private static bool IsEqual(double x, double y) =>
	        Math.Abs(x - y) < 1e-9;

        // Расстояние от точки A(ax, ay) до точки B(bx, by)
        private static double GetDistanceBetweenTwoPoints(
	        double ax, double ay, double bx, double by) =>
	        Math.Sqrt((ax - bx) * (ax - bx) + (ay - by) * (ay - by));

        // Проверяет, образуют ли точки A(ax, ay), B(bx, by) и C(cx, cy) тупой угол ABC
        private static bool AreFormObtuseAngle(
	        double ax, double ay, double bx, double by, double cx, double cy)
	    {
	        var ab = GetDistanceBetweenTwoPoints(ax, ay, bx, by);
	        var cb = GetDistanceBetweenTwoPoints(cx, cy, bx, by); 
            
            var abX = bx - ax;
	        var abY = by - ay;
	        var cbX = bx - cx;
	        var cbY = by - cy;

	        var cos = (abX * cbX + abY * cbY) / (ab * cb);

	        return cos < 0;
        }
    }
}