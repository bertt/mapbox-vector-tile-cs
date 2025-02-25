using System.Collections.Generic;

namespace Mapbox.Vector.Tile;

public class VectorTileLayer(string name, uint version, uint extent)
{
    public List<VectorTileFeature> VectorTileFeatures { get; set; } = [];
    public string Name { get; set; } = name;
    public uint Version { get; set; } = version;
    public uint Extent { get; set; } = extent;
}

