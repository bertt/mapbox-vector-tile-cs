using BenchmarkDotNet.Attributes;
using Mapbox.Vector.Tile;
using System.Collections.Generic;
using System.IO;

namespace mapbox.vector.tile.benchmark;

public class ParsingBenchmark
{
    Stream input;
    public ParsingBenchmark()
    {
        const string mapboxissue3File = @"./testdata/14-8801-5371.vector.pbf";
        input = File.OpenRead(mapboxissue3File);
    }

    [Benchmark]
    public List<VectorTileLayer> ParseVectorTileFromStream()
    {
        return VectorTileParser.Parse(input);
    }
}
