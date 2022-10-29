using GeoJSON.Net.Feature;
using System.Reflection;

namespace Mapbox.Vector.Tile.tests;

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
