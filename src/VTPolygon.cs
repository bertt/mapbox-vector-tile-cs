using System.Collections.Generic;

namespace Mapbox.Vector.Tile
{
    public class VTPolygon
    {
        private List<Coordinate> points;

        public VTPolygon(List<Coordinate> points)
        {
            this.points = points;
        }

        // method assuming polygon is closed (first point is the same as last point)
        public double SignedArea()
        {
            var sum = 0.0;
            for (var i = 0; i < points.Count-1; i++)
            {
                sum = sum + (points[i].X * points[i + 1].Y - (points[i].Y * points[i + 1].X));
            }
            return 0.5 * sum;
        }

        public bool IsOuterRing()
        {
            return IsCCW();
        }

        public bool IsCW()
        {
            return SignedArea() < 0;
        }

        public bool IsCCW()
        {
            return SignedArea() > 0;
        }

    }
}
