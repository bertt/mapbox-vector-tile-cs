using System.Collections.Generic;

namespace Mapbox.Vector.Tile;

public class VTPolygon
{
    private IList<Coordinate> points;

    public VTPolygon(IList<Coordinate> points)
    {
        this.points = points;
    }

    public double SignedArea()
    {
        double sum = 0.0;
        for (int i = 0; i < points.Count; ++i)
        {
            sum += points[i].X * (((i + 1) == points.Count) ? points[0].Y : points[i + 1].Y);
            sum -= points[i].Y * (((i + 1) == points.Count) ? points[0].X : points[i + 1].X);
        }
        return 0.5 * sum;
    }

    public bool IsOuterRing()
    {
        return IsCCW();
    }

    public bool IsCW()
    {
        return SignedArea() < 0;
    }

    public bool IsCCW()
    {
        return SignedArea() > 0;
    }
}
