using System.Collections.Generic;
using System.IO;
using GeoJSON.Net.Feature;
using ProtoBuf;

namespace mapbox.vector.tile
{
    public class VectorTileParser
    {
        public static List<LayerInfo> Parse(Stream stream)
        {
            var tile = Serializer.Deserialize<Tile>(stream);

            var lis = new List<LayerInfo>();
            foreach (var layer in tile.Layers)
            {
                var li = new LayerInfo();
                var fc = new FeatureCollection();
                foreach (var feature in layer.Features)
                {
                    var f = FeatureParser.Parse(feature, layer.Keys, layer.Values);
                    if (f != null)
                    {
                        fc.Features.Add(f);
                    }
                }
                li.FeatureCollection = fc;
                li.Name = layer.Name;
                lis.Add(li);
            }
            return lis;
        }
    }
}
