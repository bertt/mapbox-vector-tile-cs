using System;
using System.Collections.Generic;

namespace Mapbox.Vector.Tile;

public static class GeometryParser
{
    private enum Command
    {
        None = 0,
        MoveTo = 1,
        LineTo = 2,
        Bits = 3,
        SegEnd = 7,
    }

    public static List<ArraySegment<Coordinate>> ParseGeometry(IReadOnlyList<uint> geom, Tile.GeomType geomType)
    {
        long x = 0;
        long y = 0;
        
        var coords = new Coordinate[geom.Count];
        var insert = 0;
        var segmentStart = 0;

        uint length = 0;
        var command = Command.None;
        var i = 0;

        var result = new List<ArraySegment<Coordinate>>();

        while (i < coords.Length)
        {
            if (length <= 0)
            {
                length = geom[i++];
                command = (Command) (length & ((1 << 3) - 1));
                length >>= 3;
            }

            if (length > 0)
            {
                if (command == Command.MoveTo && insert > segmentStart)
                {
                    result.Add(new(coords, segmentStart, insert - segmentStart));
                    segmentStart = insert;
                }
            }

            if (command == Command.SegEnd)
            {
                if (geomType != Tile.GeomType.Point && insert > segmentStart)
                {
                    coords[insert++] = coords[segmentStart];
                }
                length--;
                continue;
            }

            var dx = geom[i++];
            var dy = geom[i++];

            length--;

            var ldx = ZigZag.Decode(dx);
            var ldy = ZigZag.Decode(dy);

            x += ldx;
            y += ldy;

            // use scale? var  coord = new Coordinate(x / scale, y / scale);
            var coord = new Coordinate(x, y);
            coords[insert++] = coord;
        }

        if (insert > segmentStart)
        {
            result.Add(new(coords, segmentStart, insert - segmentStart));            
        }

        return result;
    }
}
