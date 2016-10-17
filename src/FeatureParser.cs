using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;

namespace mapbox.vector.tile
{
    public static class FeatureParser
    {
		// todo: remove geojson.net dependency, return VectorTileFeature
		// todo: move GeoJSON.NET code to extension methods
        public static Feature Parse(Tile.Feature feature, List<string> keys, List<Tile.Value> values, int x, int y, int z, uint extent, bool convertToGeographicPosition)
        {
            Feature result = null;
            var geom = GeometryParser.ParseGeometry(feature.Geometry, feature.Type);
            var id = feature.Id;

            var positions= convertToGeographicPosition?
                geom.Select(p => p.ToGeographicPosition(x,y,z,extent)).ToList():
                geom.Select(p => new GeographicPosition(p.Latitude, p.Longitude)).ToList();

            var coordinates = positions.ToList<IPosition>();
            if (feature.Type == Tile.GeomType.Polygon)
            {
                if (coordinates.Count > 3)
                {
                    var polygon = new Polygon(new List<LineString> {new LineString(coordinates)});
                    result = new Feature(polygon);
                }
            }
            else if (feature.Type == Tile.GeomType.LineString)
            {
                var ls = new LineString(coordinates);
                result = new Feature(ls);
            }
            else if (feature.Type == Tile.GeomType.Point)
            {
                var pt = new Point(coordinates[0]);
                result = new Feature(pt);

            }

            if (result != null)
            {
                result.Id = id.ToString(CultureInfo.InvariantCulture);
                var attributes = AttributesParser.Parse(keys, values, feature.Tags);
                foreach (var item in attributes)
                {
                    result.Properties.Add(item.Key, item.Value);
                }

            }
            return result;
        }
    }
}
