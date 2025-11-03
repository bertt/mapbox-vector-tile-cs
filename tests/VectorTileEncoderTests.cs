using Mapbox.Vector.Tile;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace mapbox.vector.tile.tests;

public class VectorTileEncoderTests
{
    [Test]
    public void TestEncodeDecodeRoundTrip()
    {
        // Create a simple vector tile layer with a point feature
        var layer = new VectorTileLayer("test_layer", 2, 4096);
        
        var attributes = new List<KeyValuePair<string, object>>
        {
            new KeyValuePair<string, object>("name", "Test Point"),
            new KeyValuePair<string, object>("value", 42)
        };

        var coordinates = new[]
        {
            new Coordinate(100, 200)
        };
        
        var geometry = new List<ArraySegment<Coordinate>>
        {
            new ArraySegment<Coordinate>(coordinates)
        };

        var feature = new VectorTileFeature("1", geometry, attributes, Tile.GeomType.Point, 4096);
        layer.VectorTileFeatures.Add(feature);

        var layers = new List<VectorTileLayer> { layer };

        // Encode to stream
        var stream = new MemoryStream();
        VectorTileEncoder.Encode(layers, stream);

        // Decode from stream
        stream.Seek(0, SeekOrigin.Begin);
        var decodedLayers = VectorTileParser.Parse(stream);

        // Verify
        Assert.That(decodedLayers.Count, Is.EqualTo(1));
        Assert.That(decodedLayers[0].Name, Is.EqualTo("test_layer"));
        Assert.That(decodedLayers[0].VectorTileFeatures.Count, Is.EqualTo(1));
        
        var decodedFeature = decodedLayers[0].VectorTileFeatures[0];
        Assert.That(decodedFeature.GeometryType, Is.EqualTo(Tile.GeomType.Point));
        Assert.That(decodedFeature.Attributes.Count, Is.EqualTo(2));
    }

    [Test]
    public void TestEncodeDecodeLineString()
    {
        var layer = new VectorTileLayer("roads", 2, 4096);
        
        var attributes = new List<KeyValuePair<string, object>>
        {
            new KeyValuePair<string, object>("type", "highway")
        };

        var coordinates = new[]
        {
            new Coordinate(0, 0),
            new Coordinate(100, 0),
            new Coordinate(100, 100),
            new Coordinate(0, 100),
            new Coordinate(0, 0)
        };
        
        var geometry = new List<ArraySegment<Coordinate>>
        {
            new ArraySegment<Coordinate>(coordinates)
        };

        var feature = new VectorTileFeature("1", geometry, attributes, Tile.GeomType.LineString, 4096);
        layer.VectorTileFeatures.Add(feature);

        var layers = new List<VectorTileLayer> { layer };

        // Encode to stream
        var stream = new MemoryStream();
        VectorTileEncoder.Encode(layers, stream);

        // Decode from stream
        stream.Seek(0, SeekOrigin.Begin);
        var decodedLayers = VectorTileParser.Parse(stream);

        // Verify
        Assert.That(decodedLayers.Count, Is.EqualTo(1));
        Assert.That(decodedLayers[0].Name, Is.EqualTo("roads"));
        Assert.That(decodedLayers[0].VectorTileFeatures.Count, Is.EqualTo(1));
        
        var decodedFeature = decodedLayers[0].VectorTileFeatures[0];
        Assert.That(decodedFeature.GeometryType, Is.EqualTo(Tile.GeomType.LineString));
        Assert.That(decodedFeature.Geometry.Count, Is.EqualTo(1));
    }

    [Test]
    public void TestEncodeDecodePolygon()
    {
        var layer = new VectorTileLayer("buildings", 2, 4096);
        
        var attributes = new List<KeyValuePair<string, object>>
        {
            new KeyValuePair<string, object>("name", "Building A"),
            new KeyValuePair<string, object>("height", 25.5)
        };

        var coordinates = new[]
        {
            new Coordinate(0, 0),
            new Coordinate(100, 0),
            new Coordinate(100, 100),
            new Coordinate(0, 100),
            new Coordinate(0, 0)
        };
        
        var geometry = new List<ArraySegment<Coordinate>>
        {
            new ArraySegment<Coordinate>(coordinates)
        };

        var feature = new VectorTileFeature("1", geometry, attributes, Tile.GeomType.Polygon, 4096);
        layer.VectorTileFeatures.Add(feature);

        var layers = new List<VectorTileLayer> { layer };

        // Encode to stream
        var stream = new MemoryStream();
        VectorTileEncoder.Encode(layers, stream);

        // Decode from stream
        stream.Seek(0, SeekOrigin.Begin);
        var decodedLayers = VectorTileParser.Parse(stream);

        // Verify
        Assert.That(decodedLayers.Count, Is.EqualTo(1));
        Assert.That(decodedLayers[0].Name, Is.EqualTo("buildings"));
        Assert.That(decodedLayers[0].VectorTileFeatures.Count, Is.EqualTo(1));
        
        var decodedFeature = decodedLayers[0].VectorTileFeatures[0];
        Assert.That(decodedFeature.GeometryType, Is.EqualTo(Tile.GeomType.Polygon));
        Assert.That(decodedFeature.Attributes.Count, Is.EqualTo(2));
    }

    [Test]
    public void TestEncodeMultipleFeatures()
    {
        var layer = new VectorTileLayer("mixed", 2, 4096);
        
        // Add a point
        var pointCoords = new[] { new Coordinate(50, 50) };
        var pointGeometry = new List<ArraySegment<Coordinate>> { new ArraySegment<Coordinate>(pointCoords) };
        var pointAttrs = new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("type", "marker") };
        var pointFeature = new VectorTileFeature("1", pointGeometry, pointAttrs, Tile.GeomType.Point, 4096);
        layer.VectorTileFeatures.Add(pointFeature);

        // Add a line
        var lineCoords = new[] { new Coordinate(0, 0), new Coordinate(100, 100), new Coordinate(0, 0) };
        var lineGeometry = new List<ArraySegment<Coordinate>> { new ArraySegment<Coordinate>(lineCoords) };
        var lineAttrs = new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("type", "path") };
        var lineFeature = new VectorTileFeature("2", lineGeometry, lineAttrs, Tile.GeomType.LineString, 4096);
        layer.VectorTileFeatures.Add(lineFeature);

        var layers = new List<VectorTileLayer> { layer };

        // Encode to stream
        var stream = new MemoryStream();
        VectorTileEncoder.Encode(layers, stream);

        // Decode from stream
        stream.Seek(0, SeekOrigin.Begin);
        var decodedLayers = VectorTileParser.Parse(stream);

        // Verify
        Assert.That(decodedLayers.Count, Is.EqualTo(1));
        Assert.That(decodedLayers[0].VectorTileFeatures.Count, Is.EqualTo(2));
    }
}
