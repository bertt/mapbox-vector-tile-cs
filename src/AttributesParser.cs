using System;
using System.Collections.Generic;

namespace Mapbox.Vector.Tile;

public static class AttributesParser
{
    public static List<KeyValuePair<string, object>> Parse(List<string> keys, List<Tile.Value> values, List<uint> tags)
    {
        var result = new List<KeyValuePair<string, object>>(keys.Count);

        for (var i = 0; i < tags.Count; i += 2)
        {
            var key = keys[(int)tags[i]];
            var val = values[(int)tags[i + 1]];
            result.Add(new(key, GetAttr(val)));
        }
        return result;
    }

    private static object GetAttr(Tile.Value value) => value switch
    {
        { HasStringValue: true } => value.StringValue,
        { HasBoolValue: true } => value.BoolValue ? Boxes.Boolean_True : Boxes.Boolean_False,
        
        { HasDoubleValue: true } => value.DoubleValue switch
        {
            0 => Boxes.Double_0,
            1 => Boxes.Double_1,
            _ => value.DoubleValue,
        },
        { HasFloatValue: true } => value.FloatValue switch
        {
            0 => Boxes.Single_0,
            1 => Boxes.Single_1,
            _ => value.FloatValue,
        },
        { HasIntValue: true } => value.IntValue switch
        {
            0 => Boxes.Int64_0,
            1 => Boxes.Int64_1,
            _ => value.IntValue,
        },
        { HasSIntValue: true } => value.SintValue switch
        {
            0 => Boxes.Int64_0,
            1 => Boxes.Int64_1,
            _ => value.SintValue,
        },
        { HasUIntValue: true } => value.UintValue switch
        {
            0 => Boxes.UInt64_0,
            1 => Boxes.UInt64_1,
            _ => value.UintValue,
        },

        _ => throw new NotImplementedException("Unknown attribute type"),
    };

    private static class Boxes
    {
        public static readonly object Boolean_True = true;
        public static readonly object Boolean_False = false;
        public static readonly object Int64_0 = 0L;
        public static readonly object Int64_1 = 1L;
        public static readonly object Single_0 = 0f;
        public static readonly object Single_1 = 1f;
        public static readonly object Double_0 = 0d;
        public static readonly object Double_1 = 1d;
        public static readonly object UInt64_0 = 0ul;
        public static readonly object UInt64_1 = 1ul;
    }
}
