using System.Collections.Generic;
using System.IO;
using ProtoBuf;

namespace mapbox.vector.tile
{
    public static class VectorTileParser
    {
        public static List<VectorTileLayer> Parse(Stream stream)
        {
            var tile1 = Serializer.Deserialize<Tile>(stream);
            var list = new List<VectorTileLayer>();
            foreach (var layer in tile1.Layers)
            {
                var extent = layer.Extent;
                var vectorTileLayer = new VectorTileLayer();
                vectorTileLayer.Name = layer.Name;
                vectorTileLayer.Version = layer.Version;
                vectorTileLayer.Extent = layer.Extent;

                foreach (var feature in layer.Features)
                {
                    var vectorTileFeature = FeatureParser.ParseNew(feature, layer.Keys, layer.Values, extent);
                    vectorTileLayer.VectorTileFeatures.Add(vectorTileFeature);
                }
                list.Add(vectorTileLayer);
            }
            return list;
        }
    }
}
