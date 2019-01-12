namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength() =>
            Geometry.GetLength(this);

        public Vector Add(Vector other) =>
            Geometry.Add(this, other);

        public Vector Subtract(Vector other) =>
            Geometry.Subtract(this, other);

        public bool Belongs(Segment segment) =>
            Geometry.IsVectorInSegment(this, segment);
    }
}
