using System.Collections.Generic;
using System.IO;
using GeoJSON.Net.Feature;
using ProtoBuf;

namespace mapbox.vector.tile
{
    public class VectorTileParser
    {
        public static List<LayerInfo> Parse(Stream stream, int x, int y, int z,bool convertToGeographicPosition=true)
        {
            var tile = Serializer.Deserialize<Tile>(stream);

            var list = new List<LayerInfo>();
            foreach (var layer in tile.Layers)
            {
                var extent = layer.Extent;
                var li = new LayerInfo();
                var fc = new FeatureCollection();
                foreach (var feature in layer.Features)
                {
                    var f = FeatureParser.Parse(feature, layer.Keys, layer.Values, x, y, z, extent, convertToGeographicPosition);
                    if (f != null)
                    {
                        fc.Features.Add(f);
                    }
                }
                li.FeatureCollection = fc;
                li.Name = layer.Name;
                list.Add(li);
            }
            return list;
        }
    }
}
