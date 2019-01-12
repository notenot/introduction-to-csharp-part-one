using System;

namespace GeometryTasks
{
    public class Geometry
    {
        public static double GetLength(Vector vector) =>
            Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

        public static Vector Add(Vector one, Vector other) =>
            new Vector { X = one.X + other.X, Y = one.Y + other.Y };
    }
}
