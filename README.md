# Mapbox.Vectors

Decode a Mapbox vector tile into a collection of GeoJSON FeatureCollection objects.

For each layer there is one GeoJSON FeatureCollection.

# Usage

```cs
const string vtfile = "vectortile.pbf";

var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(vtfile);
var tile = Serializer.Deserialize<tile>(pbfStream);
var layerInfos = TileParser.Parse(tile);

Assert.IsTrue(layerInfos.Count==1);
Assert.IsTrue(layerInfos[0].FeatureCollection.Features.Count == 47);
Assert.IsTrue(layerInfos[0].FeatureCollection.Features[0].Geometry.Type == GeoJSONObjectType.Polygon);
Assert.IsTrue(layerInfos[0].FeatureCollection.Features[0].Properties.Count==2);
```

