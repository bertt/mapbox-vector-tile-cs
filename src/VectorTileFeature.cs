using System;
using System.Collections.Generic;

namespace Mapbox.Vector.Tile;

public class VectorTileFeature
{
    public string Id { get; set; }
    public List<ArraySegment<Coordinate>> Geometry { get; set; }
    public List<KeyValuePair<string, object>> Attributes { get; set; }
    public Tile.GeomType GeometryType { get; set; }
    public uint Extent { get; set; }
}

