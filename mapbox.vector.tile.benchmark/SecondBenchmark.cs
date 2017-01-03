using BenchmarkDotNet.Attributes;
using GeoJSON.Net.Feature;
using Mapbox.Vector.Tile;
using System.Collections.Generic;
using System.IO;

namespace mapbox.vector.tile.benchmark
{
    public class SecondBenchmark
    {
        List<VectorTileLayer> layers;
        public SecondBenchmark()
        {
            const string mapboxissue3File = @"./testdata/14-8801-5371.vector.pbf";
            var input = File.OpenRead(mapboxissue3File);
            layers = VectorTileParser.Parse(input);
        }

        [Benchmark]
        public FeatureCollection VectorTilePointLayerToGeoJSON()
        {
            return layers[17].ToGeoJSON(8801, 5371, 14);
        }

        [Benchmark]
        public FeatureCollection VectorTileLineLayerToGeoJSON()
        {
            return layers[8].ToGeoJSON(8801, 5371, 14);
        }

        [Benchmark]
        public FeatureCollection VectorTilePolygonLayerToGeoJSON()
        {
            return layers[5].ToGeoJSON(8801, 5371, 14);
        }


    }
}
