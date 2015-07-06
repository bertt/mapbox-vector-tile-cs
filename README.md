# Mapbox.Vectors

Decode a Mapbox vector tile into a collection of GeoJSON FeatureCollection objects.

For each layer there is one GeoJSON FeatureCollection. Code is tested using the Mapbox tests from

https://github.com/mapbox/vector-tile-js

# Usage

```cs
const string vtfile = "vectortile.pbf";

var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(vtfile);
var layerInfos = TileParser.Parse(pbfStream);

Assert.IsTrue(layerInfos.Count==1);
Assert.IsTrue(layerInfos[0].FeatureCollection.Features.Count == 47);
Assert.IsTrue(layerInfos[0].FeatureCollection.Features[0].Geometry.Type == GeoJSONObjectType.Polygon);
Assert.IsTrue(layerInfos[0].FeatureCollection.Features[0].Properties.Count==2);
```

See also https://github.com/bertt/Mapbox.Vectors/blob/master/tests/TileParserTests.cs for working examples

