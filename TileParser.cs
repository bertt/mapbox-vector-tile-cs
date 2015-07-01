using System.Collections.Generic;
using GeoJSON.Net.Feature;
using Mapbox.Vectors.mapnik.vector;

namespace Mapbox.Vectors
{
    public class TileParser
    {
        public static List<LayerInfo> Parse(tile tile)
        {
            var lis = new List<LayerInfo>();
            foreach (var layer in tile.layers)
            {
                var li = new LayerInfo();
                var fc = new FeatureCollection();
                foreach (var feature in layer.features)
                {
                    var f = FeatureParser.Parse(feature, layer.keys, layer.values);
                    if (f != null)
                    {
                        fc.Features.Add(f);
                    }
                }
                li.FeatureCollection = fc;
                li.Name = layer.name;
                lis.Add(li);
            }
            return lis;
        }
    }
}
