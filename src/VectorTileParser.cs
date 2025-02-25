using ProtoBuf;
using System.Collections.Generic;
using System.IO;

namespace Mapbox.Vector.Tile;

public static class VectorTileParser
{
    public static List<VectorTileLayer> Parse(Stream stream)
    {
        var tile = Serializer.Deserialize<Tile>(stream);
        var list = new List<VectorTileLayer>();
        foreach (var layer in tile.Layers)
        {
            var extent = layer.Extent;
            var vectorTileLayer = new VectorTileLayer(layer.Name ?? "[Unnamed]", layer.Version, extent);

            foreach (var feature in layer.Features)
            {
                var vectorTileFeature = FeatureParser.Parse(feature, layer.Keys, layer.Values, extent);
                vectorTileLayer.VectorTileFeatures.Add(vectorTileFeature);
            }
            list.Add(vectorTileLayer);
        }
        return list;
    }
}
