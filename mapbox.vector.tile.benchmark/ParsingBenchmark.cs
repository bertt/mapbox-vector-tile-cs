using BenchmarkDotNet.Attributes;
using Mapbox.Vector.Tile;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace mapbox.vector.tile.benchmark
{
    public class ParsingBenchmark
    {
        Stream input;
        public ParsingBenchmark()
        {
            const string mapboxissue3File = "mapbox.vector.tile.benchmark.testdata.14-8801-5371.vector.pbf";
            input = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxissue3File);
        }

        [Benchmark]
        public List<VectorTileLayer> ParseVectorTileFromStream()
        {
            return VectorTileParser.Parse(input);
        }
    }
}
