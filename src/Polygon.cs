using Mapbox.Vector.Tile;
using System.Collections.Generic;

namespace mapbox.vector.tile.tests
{
    public class Polygon
    {
        private List<Coordinate> points;

        public Polygon(List<Coordinate> points)
        {
            this.points = points;
        }

        // method assuming polygon is closed (first point is the same as last point)
        public double SignedArea()
        {
            double sum = 0.0;
            for (int i = 0; i < points.Count-1; i++)
            {
                sum = sum + (points[i].X * points[i + 1].Y - (points[i].Y * points[i + 1].X));
            }
            return 0.5 * sum;
        }

        public bool isCCW()
        {
            return SignedArea() > 0;
        }

        public bool isCW()
        {
            return SignedArea() < 0;
        }

    }
}
