using System;

namespace GeometryTasks
{
    public class Geometry
    {
        public static double GetLength(Vector vector) =>
            Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

        public static double GetLength(Segment segment) =>
            GetLength(Subtract(segment.End, segment.Begin));

        public static Vector Add(Vector one, Vector other) =>
            new Vector { X = one.X + other.X, Y = one.Y + other.Y };

        public static Vector Subtract(Vector one, Vector other) =>
            new Vector { X = one.X - other.X, Y = one.Y - other.Y };

        public static bool IsVectorInSegment(Vector vector, Segment segment) =>
            Math.Abs(GetLength(Subtract(vector, segment.Begin)) + 
                     GetLength(Subtract(vector, segment.End)) - 
                     GetLength(segment)) < double.Epsilon;
    }
}
