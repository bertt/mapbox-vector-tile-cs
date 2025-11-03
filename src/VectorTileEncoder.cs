using ProtoBuf;
using System.Collections.Generic;
using System.IO;

namespace Mapbox.Vector.Tile;

public static class VectorTileEncoder
{
    public static void Encode(List<VectorTileLayer> layers, Stream stream)
    {
        var tile = new Tile();
        
        foreach (var vectorTileLayer in layers)
        {
            var layer = new Tile.Layer
            {
                Name = vectorTileLayer.Name,
                Version = vectorTileLayer.Version,
                Extent = vectorTileLayer.Extent
            };

            var keys = new List<string>();
            var values = new List<Tile.Value>();

            foreach (var vectorTileFeature in vectorTileLayer.VectorTileFeatures)
            {
                var feature = new Tile.Feature
                {
                    Id = ulong.Parse(vectorTileFeature.Id),
                    Type = vectorTileFeature.GeometryType
                };

                // Encode attributes
                var tags = AttributesEncoder.Encode(vectorTileFeature.Attributes, keys, values);
                feature.Tags.AddRange(tags);

                // Encode geometry
                var geometry = GeometryEncoder.EncodeGeometry(vectorTileFeature.Geometry, vectorTileFeature.GeometryType);
                feature.Geometry.AddRange(geometry);

                layer.Features.Add(feature);
            }

            layer.Keys.AddRange(keys);
            layer.Values.AddRange(values);

            tile.Layers.Add(layer);
        }

        Serializer.Serialize(stream, tile);
    }
}
