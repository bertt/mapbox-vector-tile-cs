namespace Mapbox.Vector.Tile;

public struct Coordinate(long x, long y)
{
    public long X { get; set; } = x; 
    public long Y { get; set; } = y;
}
