namespace GeometryTasks
{
    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public double GetLength() =>
            Geometry.GetLength(this);

        public bool Contains(Vector vector) =>
            Geometry.IsVectorInSegment(vector, this);
    }
}
