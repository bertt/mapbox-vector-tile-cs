using Mapbox.Vector.Tile;
using Newtonsoft.Json;

// sample converting a vector tile to GeoJSON
Console.WriteLine("Converting a vector tile to GeoJSON");
string mvtFile = @"./testfixtures/14-8801-5371.vector.pbf";
var input = File.OpenRead(mvtFile);
var layers = VectorTileParser.Parse(input);
var geoJson = layers[5].ToGeoJSON(8801, 5371, 14);
var json = JsonConvert.SerializeObject(geoJson);
File.WriteAllText("output.geojson", json);
Console.WriteLine("GeoJSON written to output.geojson");
