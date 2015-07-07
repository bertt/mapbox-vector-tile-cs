# mapbox-vector-tile-cs 

Decode a Mapbox vector tile into a collection of GeoJSON FeatureCollection objects.

For each layer there is one GeoJSON FeatureCollection. Code is tested using the Mapbox tests from

https://github.com/mapbox/vector-tile-js

# Usage

```cs
const string vtfile = "vectortile.pbf";
using (var stream = File.OpenRead(vtfile))
{
  var layerInfos = VectorTileParser.Parse(stream);

  Assert.IsTrue(layerInfos.Count==1);
  Assert.IsTrue(layerInfos[0].FeatureCollection.Features.Count == 47);
  Assert.IsTrue(layerInfos[0].FeatureCollection.Features[0].Geometry.Type == GeoJSONObjectType.Polygon);
  Assert.IsTrue(layerInfos[0].FeatureCollection.Features[0].Properties.Count==2);
}
```

See also https://github.com/bertt/mapbox-vector-tile-cs/blob/master/tests/mapbox.vector.tile.tests/TileParserTests.cs for working examples

