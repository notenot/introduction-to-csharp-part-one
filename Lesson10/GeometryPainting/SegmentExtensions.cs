using System.Collections.Generic;
using System.Drawing;
using GeometryTasks;

namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        public static Dictionary<Segment, Color> SegmentColors =
            new Dictionary<Segment, Color>();

        public static Color GetColor(this Segment segment) =>
            SegmentColors.ContainsKey(segment) ? SegmentColors[segment] : Color.Black;

        public static void SetColor(this Segment segment, Color color)
        {
            if (SegmentColors.ContainsKey(segment))
            {
                SegmentColors[segment] = color;
                return;
            }
            SegmentColors.Add(segment, color);
        }
    }
}
