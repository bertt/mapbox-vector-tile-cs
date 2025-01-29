using GeoJSON.Net.Geometry;

namespace Mapbox.Vector.Tile;

public static class CoordinateExtensions
{
    public static Position ToPosition(this Coordinate c, int x, int y, int z, uint extent)
    {
        var size = extent * Math.Pow(2, z);
        var x0 = extent * x;
        var y0 = extent * y;

        var y2 = 180 - (c.Y + y0) * 360 / size;
        var lon = (c.X + x0) * 360 / size - 180;
        var lat = 360 / Math.PI * Math.Atan(Math.Exp(y2 * Math.PI / 180)) - 90;

        return new Position(lat, lon);
    }
}
