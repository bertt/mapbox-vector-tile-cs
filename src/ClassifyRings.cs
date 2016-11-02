using System.Collections.Generic;

namespace Mapbox.Vector.Tile
{
    public class ClassifyRings
    {
        public static List<List<List<Coordinate>>> Classify(List<List<Coordinate>> rings)
        {
            bool ccw;
            var polygons = new List<List<List<Coordinate>>>();
            foreach (var ring in rings)
            {
                var poly = new VTPolygon(ring);
                if (poly.isCW())
                {
                    var newpoly = new List<List<Coordinate>>() { ring };
                    polygons.Add(newpoly);
                }
            }

            return polygons;
        }
    }
}
