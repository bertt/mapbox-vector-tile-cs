using System.Collections.Generic;
using System.IO;
using GeoJSON.Net.Feature;
using Mapbox.Vectors.mapnik.vector;
using ProtoBuf;

namespace Mapbox.Vectors
{
    public class VectorTileParser
    {
        public static List<LayerInfo> Parse(Stream stream)
        {
            var tile = Serializer.Deserialize<tile>(stream);

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
