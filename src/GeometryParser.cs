using System;
using System.Collections.Generic;

namespace mapbox.vector.tile
{
    public static class GeometryParser
    {
        public static List<List<Coordinate>> ParseGeometryNew1(List<uint> geom, Tile.GeomType geomType)
        {
            const uint cmdMoveTo = 1;
            //const uint cmdLineTo = 2;
            const uint cmdSegEnd = 7;
            //const uint cmdBits = 3;

            long x = 0;
            long y = 0;
            var coordsList = new List<List<Coordinate>>();
            List<Coordinate> coords = null;
            int geometryCount = geom.Count;
            uint length = 0;
            uint command = 0;
            int i = 0;
            while (i < geometryCount)
            {
                if (length <= 0)
                {
                    length = geom[i++];
                    command = length & ((1 << 3) - 1);
                    length = length >> 3;
                }

                if (length > 0)
                {
                    if (command == cmdMoveTo)
                    {
                        coords = new List<Coordinate>();
                        coordsList.Add(coords);
                    }
                }

                if (command == cmdSegEnd)
                {
                    if (geomType != Tile.GeomType.Point && !(coords.Count == 0))
                    {
                        coords.Add(coords[0]);
                    }
                    length--;
                    continue;
                }

                uint dx = geom[i++];
                uint dy = geom[i++];

                length--;

                var ldx = ZigZag.Decode(dx);
                var ldy = ZigZag.Decode(dy);

                x = x + ldx;
                y = y + ldy;

                // use scale? var  coord = new Coordinate(x / scale, y / scale);
                var  coord = new Coordinate() { Longitude = x, Latitude = y };
                coords.Add(coord);
            }
            return coordsList;
        }

        public static IEnumerable<Coordinate> ParseGeometry(List<uint> geom, Tile.GeomType geomType)
        {
            var i = 0;
            long dx = 0;
            long dy = 0;

            const int cmdMoveTo = 1;
            const int cmdLineTo = 2;
            const int cmdSegEnd = 7;
            const int cmdBits = 3;
            var coords = new List<Coordinate>();

            while (i != geom.Count)
            {
                var item = "0b" + Convert.ToString(geom[i], 2);
                var ilen = item.Length;
                var subs = item.Substring(ilen - cmdBits, cmdBits);
                var cmd = Convert.ToInt32(subs, 2);
                var subs1 = item.Substring(2, ilen - cmdBits - 2);
                var cmdLength = Convert.ToInt32(subs1, 2);

                i = i + 1;
                if (cmd == cmdSegEnd) break;
                if (cmd == cmdMoveTo || cmd == cmdLineTo)
                {
                    for (var j = 0; j < cmdLength; j++)
                    {
                        var xin = geom[i];
                        i = i + 1;

                        var yin = geom[i];
                        i = i + 1;

                        var x = ZigZag.Decode(xin);
                        var y = ZigZag.Decode(yin);
                        x = x + dx;
                        y = y + dy;

                        dx = x;
                        dy = y;
                        coords.Add(new Coordinate { Longitude = x, Latitude = y });
                    }
                }
            }

            if (geomType == Tile.GeomType.Polygon && coords.Count > 0)
            {
                coords.Add(coords[0]);
            }

            return coords;
        }
    }
}
