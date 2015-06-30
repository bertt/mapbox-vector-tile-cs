using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Mapbox.Vectors.mapnik.vector;

namespace Mapbox.Vectors
{
    public class FeatureParser
    {
        public static Feature Parse(tile.feature feature)
        {
            Feature result = null;
            var geom = GeometryParser.ParseGeometry(feature.geometry, feature.type);
            var id = feature.id;

            var positions = geom.Select(p => new GeographicPosition(p.Longitude, p.Latitude)).ToList();

            var coordinates = positions.ToList<IPosition>();
            if (feature.type == tile.GeomType.Polygon)
            {
                if (coordinates.Count > 3)
                {
                    var polygon = new Polygon(new List<LineString> {new LineString(coordinates)});
                    result = new Feature(polygon);
                }
            }
            else if (feature.type == tile.GeomType.LineString)
            {
                var ls = new LineString(coordinates);
                result = new Feature(ls);
            }
            else if (feature.type == tile.GeomType.Point)
            {
                var pt = new Point(coordinates[0]);
                result = new Feature(pt);

            }
            // todo do something with tags
            if (result != null)
            {
                result.Id = id.ToString(CultureInfo.InvariantCulture);
            }
            return result;
        }

    }
}
