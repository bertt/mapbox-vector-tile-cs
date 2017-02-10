using System.Collections.Generic;

namespace Mapbox.Vector.Tile
{
    public class ClassifyRings
    {
        // docs for inner/outer rings https://www.mapbox.com/vector-tiles/specification/
        public static List<List<List<Coordinate>>> Classify(List<List<Coordinate>> rings)
        {
            var polygons = new List<List<List<Coordinate>>>();
            List<List<Coordinate>> newpoly = null;
            foreach (var ring in rings)
            {
                var poly = new VTPolygon(ring);

                if (poly.IsOuterRing())
                {
                    newpoly = new List<List<Coordinate>>() { ring };
                    polygons.Add(newpoly);
                }
                else
                {
                    newpoly?.Add(ring);
                }
            }

            return polygons;
        }
    }
}
