using System;
using System.Collections.Generic;

namespace Mapbox.Vector.Tile;

public class VectorTileFeature(string id, List<ArraySegment<Coordinate>> geometry, List<KeyValuePair<string, object>> attributes, Tile.GeomType geometryType, uint extent)
{
    public string Id { get; set; } = id;
    public List<ArraySegment<Coordinate>> Geometry { get; set; } = geometry;
    public List<KeyValuePair<string, object>> Attributes { get; set; } = attributes;
    public Tile.GeomType GeometryType { get; set; } = geometryType;
    public uint Extent { get; set; } = extent;
}

