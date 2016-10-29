using System.Collections.Generic;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using mapbox.vector.tile.ExtensionMethods;
using Microsoft.VisualBasic.CompilerServices;

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

        public static Feature ToGeoJSON(this VectorTileFeature vectortileFeature, int x, int y, int z, uint extent)
        {
            // var f = new Feature();

            switch (vectortileFeature.GeometryType)
            {
                case Tile.GeomType.Point:
                    var coords = new List<Coordinate>();
                    var projectedCoords = new List<GeographicPosition>();

                    foreach (var g in vectortileFeature.Geometry)
                    {
                        coords.Add(g[0]);
                        projectedCoords = Project(coords,x,y,z, extent);
                    }
                    if (projectedCoords.Count == 1)
                    {
                        //var geom = new Point(x);

                    }
                    else
                    {
                        // create multipoint
                    }


                    break;
                case Tile.GeomType.LineString:
                    break;
                case Tile.GeomType.Polygon:
                    break;
            }


            // todo: create convertor
            return null;
        }
    }
}