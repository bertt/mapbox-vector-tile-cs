# mapbox-vector-tile-cs 

[![NuGet Status](http://img.shields.io/nuget/v/mapbox-vector-tile.svg?style=flat)](https://www.nuget.org/packages/mapbox-vector-tile/)

A .NET library for decoding a Mapbox vector tile into a collection of GeoJSON FeatureCollection objects.

For each layer there is one GeoJSON FeatureCollection. Code is tested using the Mapbox tests from

https://github.com/mapbox/vector-tile-js

Dependencies: GeoJSON.NET, JSON.NET, protobuf-net

###Get it from NuGet 
`
PM> Install-Package mapbox-vector-tile
`

https://www.nuget.org/packages/mapbox-vector-tile

# Usage

```cs
const string vtfile = "vectortile.pbf";
using (var stream = File.OpenRead(vtfile))
{
  // parameters: tileColumn = 67317, tileRow = 43082, tileLevel = 17 
  var layerInfos = VectorTileParser.Parse(stream,67317,43082,17);

  Assert.IsTrue(layerInfos.Count==1);
  Assert.IsTrue(layerInfos[0].FeatureCollection.Features.Count == 47);
  Assert.IsTrue(layerInfos[0].FeatureCollection.Features[0].Geometry.Type == GeoJSONObjectType.Polygon);
  Assert.IsTrue(layerInfos[0].FeatureCollection.Features[0].Properties.Count==2);
}
```

See also https://github.com/bertt/mapbox-vector-tile-cs/blob/master/tests/mapbox.vector.tile.tests/TileParserTests.cs for working examples

