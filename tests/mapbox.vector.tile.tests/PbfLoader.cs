using GeoJSON.Net.Feature;
using System.Reflection;
using mapbox.vector.tile.ExtensionMethods;

namespace mapbox.vector.tile.tests
{
    public class PbfLoader
    {
        public static Feature GeoJSONFromFixture(string name)
        {
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
            var layerInfos = VectorTileParser.Parse(pbfStream);
            var test = layerInfos[0].VectorTileFeatures[0].ToGeoJSON(0, 0, 0);
            return test;
        }

    }
}
