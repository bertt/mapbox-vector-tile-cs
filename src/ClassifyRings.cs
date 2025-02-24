using System.Collections.Generic;

namespace Mapbox.Vector.Tile;

public class ClassifyRings
{
    // docs for inner/outer rings https://www.mapbox.com/vector-tiles/specification/
    public static List<List<IList<Coordinate>>> Classify(IEnumerable<IList<Coordinate>> rings)
    {
        var polygons = new List<List<IList<Coordinate>>>();
        List<IList<Coordinate>> newpoly = null;
        foreach (var ring in rings)
        {
            var poly = new VTPolygon(ring);

            if (poly.IsOuterRing())
            {
                newpoly = [ring];
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
