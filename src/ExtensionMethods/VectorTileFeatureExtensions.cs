using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;

namespace Mapbox.Vector.Tile
{
    public static class VectorTileFeatureExtensions
    {
        private static List<Position> Project(List<Coordinate> coords, int x, int y, int z, uint extent)
        {
            return coords.Select(coord => coord.ToPosition(x, y, z, extent)).ToList();
        }

        private static LineString CreateLineString(List<Position> pos)
        {
            return new LineString(pos);
        }


        private static IGeometryObject GetPointGeometry(List<Position> pointList)
        {
            IGeometryObject geom;
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

        private static List<LineString> GetLineStringList(List<List<Position>> pointList)
        {
            return pointList.Select(part => CreateLineString(part)).ToList();
        }

        private static IGeometryObject GetLineGeometry(List<List<Position>> pointList)
        {
            IGeometryObject geom;

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

        private static Polygon GetPolygon(List<List<Position>> lines)
        {
            var res = new List<LineString>();
            foreach (var innerring in lines)
            {
                var line = new LineString(innerring);
                if (line.IsLinearRing() && line.IsClosed())
                {
                    res.Add(line);
                }
            }
            var geom = new Polygon(res);
            return geom;
        }

        private static IGeometryObject GetPolygonGeometry(List<List<List<Position>>> polygons)
        {
            {
                IGeometryObject geom=null;

                if (polygons.Count == 1)
                {
                    geom = GetPolygon(polygons[0]);
                }
                else if(polygons.Count>0)
                {
                    var multipolys = new List<Polygon>();
                    foreach(var poly in polygons)
                    {
                        var pl = GetPolygon(poly);
                        multipolys.Add(pl);
                    }

                    geom = new MultiPolygon(multipolys);
                }
                return geom;
            }
        }

        public static List<Position> ProjectPoints(List<List<Coordinate>> Geometry, int x, int y, int z, uint extent)
        {
            var projectedCoords = new List<Position>();
            var coords = new List<Coordinate>();

            foreach (var g in Geometry)
            {
                coords.Add(g[0]);
                projectedCoords = Project(coords, x, y, z, extent);
            }
            return projectedCoords;
        }

        public static List<List<Position>> ProjectLines(List<List<Coordinate>> Geometry, int x, int y, int z, uint extent)
        {
            var projectedCoords = new List<Position>();
            var pointList = new List<List<Position>>();
            foreach (var g in Geometry)
            {
                projectedCoords = Project(g, x, y, z, extent);
                pointList.Add(projectedCoords);
            }
            return pointList;
        }

        public static List<List<List<Position>>> ProjectPolygons(List<List<List<Coordinate>>> Geometry, int x, int y, int z, uint extent)
        {
            var projectedCoords = new List<List<Position>>();
            var result = new List<List<List<Position>>>();
            foreach (var g in Geometry)
            {
                projectedCoords = ProjectLines(g, x, y, z, extent);
                result.Add(projectedCoords);
            }
            return result;
        }


        public static Feature ToGeoJSON(this VectorTileFeature vectortileFeature, int x, int y, int z)
        {
            IGeometryObject geom = null;

            switch (vectortileFeature.GeometryType)
            {
                case Tile.GeomType.Point:
                    var projectedPoints = ProjectPoints(vectortileFeature.Geometry, x, y, z, vectortileFeature.Extent);
                    geom = GetPointGeometry(projectedPoints);
                    break;
                case Tile.GeomType.LineString:
                    var projectedLines = ProjectLines(vectortileFeature.Geometry, x, y, z, vectortileFeature.Extent);
                    geom = GetLineGeometry(projectedLines);
                    break;
                case Tile.GeomType.Polygon:
                    var rings = ClassifyRings.Classify(vectortileFeature.Geometry);
                    var projectedPolygons = ProjectPolygons(rings, x, y, z, vectortileFeature.Extent);
                    geom = GetPolygonGeometry(projectedPolygons);
                    break;
            }

            var result = new Feature(geom, id: vectortileFeature.Id);

            // add attributes
            foreach (var item in vectortileFeature.Attributes)
            {
                result.Properties.Add(item.Key, item.Value);

            }
            return result;
        }
    }
}
