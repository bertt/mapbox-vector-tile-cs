using BenchmarkDotNet.Attributes;
using Mapbox.Vector.Tile;
using System;
using System.Collections.Generic;
using System.IO;

namespace mapbox.vector.tile.benchmark;

/// <summary>
/// Benchmarks parsing of a tile with many boolean attributes, to measure
/// the impact of the pre-boxed Boxes.Boolean_True/False optimisation.
/// </summary>
[MemoryDiagnoser]
public class BooleanAttributesBenchmark
{
    private MemoryStream _tileStream = null!;

    [GlobalSetup]
    public void Setup()
    {
        const int featureCount = 100_000;
        const uint extent = 4096;

        var attributes = new List<KeyValuePair<string, object>>
        {
            new("is_active",    true),
            new("is_visible",   true),
            new("is_selected",  false),
            new("has_children", false),
            new("is_root",      true),
        };

        var features = new List<VectorTileFeature>(featureCount);
        for (var i = 0; i < featureCount; i++)
        {
            var geometry = new List<ArraySegment<Coordinate>>
            {
                new([new Coordinate { X = 0, Y = 0 }]),
            };
            features.Add(new VectorTileFeature(
                id: i.ToString(),
                geometry: geometry,
                attributes: attributes,
                geometryType: Tile.GeomType.Point,
                extent: extent));
        }

        var layer = new VectorTileLayer("bool_layer", 2, extent)
        {
            VectorTileFeatures = features,
        };

        var buffer = new MemoryStream();
        VectorTileEncoder.Encode([layer], buffer);
        _tileStream = new MemoryStream(buffer.ToArray());
    }

    [IterationSetup]
    public void ResetStream() => _tileStream.Position = 0;

    [Benchmark]
    public List<VectorTileLayer> ParseBooleanAttributeTile()
    {
        _tileStream.Position = 0;
        return VectorTileParser.Parse(_tileStream);
    }
}
