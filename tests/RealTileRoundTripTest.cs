using Mapbox.Vector.Tile;
using NUnit.Framework;
using System.IO;

namespace mapbox.vector.tile.tests;

public class RealTileRoundTripTest
{
    [Test]
    public void TestRealTileRoundTrip()
    {
        // Test that we can decode a real tile and encode it back
        var pbfFile = Path.Combine("testdata", "14-8801-5371.vector.pbf");
        
        // Decode the original tile
        var originalStream = File.OpenRead(pbfFile);
        var decodedLayers = VectorTileParser.Parse(originalStream);
        originalStream.Close();

        Assert.That(decodedLayers.Count, Is.GreaterThan(0), "Should have decoded at least one layer");

        // Encode it back
        var encodedStream = new MemoryStream();
        VectorTileEncoder.Encode(decodedLayers, encodedStream);
        encodedStream.Seek(0, SeekOrigin.Begin);

        Assert.That(encodedStream.Length, Is.GreaterThan(0), "Encoded stream should not be empty");

        // Decode the encoded tile
        var reDecodedLayers = VectorTileParser.Parse(encodedStream);

        // Verify layer count matches
        Assert.That(reDecodedLayers.Count, Is.EqualTo(decodedLayers.Count), "Layer count should match");

        // Verify each layer
        for (int i = 0; i < decodedLayers.Count; i++)
        {
            var original = decodedLayers[i];
            var reDecoded = reDecodedLayers[i];
            
            Assert.That(reDecoded.Name, Is.EqualTo(original.Name), $"Layer {i} name should match");
            Assert.That(reDecoded.VectorTileFeatures.Count, Is.EqualTo(original.VectorTileFeatures.Count), 
                $"Layer '{original.Name}' feature count should match");
        }
    }
}
