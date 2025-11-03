using System;
using System.Collections.Generic;
using System.Linq;

namespace Mapbox.Vector.Tile;

public static class GeometryEncoder
{
    private enum Command
    {
        MoveTo = 1,
        LineTo = 2,
        ClosePath = 7
    }

    public static List<uint> EncodeGeometry(List<ArraySegment<Coordinate>> geometry, Tile.GeomType geomType)
    {
        var commands = new List<uint>();
        long lastX = 0;
        long lastY = 0;

        foreach (var segment in geometry)
        {
            if (segment.Count == 0)
                continue;

            // Access the underlying array and use offset
            var array = segment.Array!;
            var offset = segment.Offset;
            var count = segment.Count;

            // MoveTo command for first point
            var firstPoint = array[offset];
            commands.Add(EncodeCommand(Command.MoveTo, 1));
            
            var dx = firstPoint.X - lastX;
            var dy = firstPoint.Y - lastY;
            commands.Add((uint)ZigZag.Encode(dx));
            commands.Add((uint)ZigZag.Encode(dy));
            
            lastX = firstPoint.X;
            lastY = firstPoint.Y;

            // LineTo commands for remaining points (excluding last if it's a duplicate of first)
            var lineToCount = count - 1;
            
            // For polygons, check if last point equals first point
            if (geomType == Tile.GeomType.Polygon && count > 1)
            {
                var lastPoint = array[offset + count - 1];
                if (lastPoint.X == firstPoint.X && lastPoint.Y == firstPoint.Y)
                {
                    lineToCount--;
                }
            }

            if (lineToCount > 0)
            {
                commands.Add(EncodeCommand(Command.LineTo, (uint)lineToCount));

                for (int i = 1; i <= lineToCount; i++)
                {
                    var point = array[offset + i];
                    dx = point.X - lastX;
                    dy = point.Y - lastY;
                    commands.Add((uint)ZigZag.Encode(dx));
                    commands.Add((uint)ZigZag.Encode(dy));
                    
                    lastX = point.X;
                    lastY = point.Y;
                }
            }

            // ClosePath command for polygons and linestrings
            if (geomType != Tile.GeomType.Point)
            {
                commands.Add(EncodeCommand(Command.ClosePath, 1));
            }
        }

        return commands;
    }

    private static uint EncodeCommand(Command command, uint count)
    {
        return (count << 3) | (uint)command;
    }
}
