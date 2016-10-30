using System.Collections.Generic;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using mapbox.vector.tile.ExtensionMethods;
using System.Linq;

namespace mapbox.vector.tile
{
    public static class VectorTileFeatureExtensions
    {

        private static List<GeographicPosition> Project(List<Coordinate> coords, int x, int y, int z, uint extent)
        {
            var projectedCoords = new List<GeographicPosition>();
            foreach (var coord in coords)
            {
                projectedCoords.Add(coord.ToGeographicPosition(x,y,z, extent));
            }
            return projectedCoords;
        }

        private static LineString CreateLineString(List<GeographicPosition> pos)
        {
            var pnts = pos.Select(p => new Point(p)).ToList();
            var ls = new LineString(pos);
            return ls;
        }
        private static IGeometryObject GetPointGeometry(List<GeographicPosition> pointList)
        {
            IGeometryObject geom = null;
            if (pointList.Count == 1)
            {
                geom = new Point(pointList[0]);
            }
            else
            {
                var pnts = pointList.Select(p => new Point(p)).ToList();
                geom = new MultiPoint(pnts);
            }
            return geom;
        }

        private static List<LineString> GetLineStringList(List<List<GeographicPosition>> pointList)
        {
            var lines = new List<LineString>();

            foreach (var part in pointList)
            {
                var ls = CreateLineString(part);
                lines.Add(ls);
            }
            return lines;
        }

        private static IGeometryObject GetLineGeometry(List<List<GeographicPosition>> pointList)
        {
            IGeometryObject geom = null;

            if (pointList.Count == 1)
            {
                geom = new LineString(pointList[0]);
            }
            else
            {
                geom = new MultiLineString(GetLineStringList(pointList));
            }
            return geom;
        }

        public static List<GeographicPosition> ProjectPoints(List<List<Coordinate>> Geometry, int x, int y, int z, uint extent)
        {
            var projectedCoords = new List<GeographicPosition>();
            var coords = new List<Coordinate>();

            foreach (var g in Geometry)
            {
                coords.Add(g[0]);
                projectedCoords = Project(coords, x, y, z, extent);
            }
            return projectedCoords;
        }

        public static List<List<GeographicPosition>> ProjectLines(List<List<Coordinate>> Geometry, int x, int y, int z, uint extent)
        {
            var projectedCoords = new List<GeographicPosition>();
            var pointList = new List<List<GeographicPosition>>();
            foreach (var g in Geometry)
            {
                projectedCoords = Project(g, x, y, z, extent);
                pointList.Add(projectedCoords);
            }
            return pointList;
        }

        public static Feature ToGeoJSON(this VectorTileFeature vectortileFeature, int x, int y, int z, uint extent)
        {
            Feature result = null;
            IGeometryObject geom = null;

            switch (vectortileFeature.GeometryType)
            {
                case Tile.GeomType.Point:
                    var projectedPoints = ProjectPoints(vectortileFeature.Geometry, x, y, z, extent);
                    geom = GetPointGeometry(projectedPoints);
                    break;
                case Tile.GeomType.LineString:
                    var projectedLines = ProjectLines(vectortileFeature.Geometry, x, y, z, extent);
                    geom = GetLineGeometry(projectedLines);
                    break;
                case Tile.GeomType.Polygon:
                    var projectedPolygons = ProjectLines(vectortileFeature.Geometry, x, y, z, extent);
                    // todo: inner/outer linearrings, multipolygons...
                    if (projectedPolygons.Count <= 1)
                    {
                    }
                    break;
            }

            result = new Feature(geom);

            // add attributes
            foreach (var item in vectortileFeature.Attributes)
            {
                result.Properties.Add(item.Key, item.Value);

            }
            result.Id = vectortileFeature.Id;
            return result;
        }
    }
}