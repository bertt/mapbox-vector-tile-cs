using GeoJSON.Net.Feature;

namespace mapbox.vector.tile
{
	public static class VectorTileLayerExtensions
	{
		public static FeatureCollection ToGeoJSON(this VectorTileLayer vectortileLayer, int x, int y, int z)
		{
		    var featureCollection = new FeatureCollection();

            foreach (var feature in vectortileLayer.VectorTileFeatures)
            {
                var geojsonFeature = feature.ToGeoJSON(x,y,z);
                featureCollection.Features.Add(geojsonFeature);
            }
		    return featureCollection;
		}
	}
}

