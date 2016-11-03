using BenchmarkDotNet.Attributes;
using Mapbox.Vector.Tile;
using System.Collections.Generic;
using System.Reflection;

namespace mapbox.vector.tile.tests
{
    public class BenchmarkTests
    {
        [Benchmark]
        public List<VectorTileLayer> DoParse()
        {
            // arrange
            const string mapboxissue3File = "mapbox.vector.tile.benchmark.testdata.mapbox_tile.vector.pbf";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapboxissue3File);

            return VectorTileParser.Parse(pbfStream);
        }

    }
}
