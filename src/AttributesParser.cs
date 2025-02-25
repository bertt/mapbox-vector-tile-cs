using System;
using System.Collections.Generic;
using System.Linq;

namespace Mapbox.Vector.Tile;

public static class AttributesParser
{
    public static List<KeyValuePair<string, object>> Parse(List<string> keys, List<Tile.Value> values, List<uint> tags)
    {
        var result = new List<KeyValuePair<string, object>>();
        var odds = tags.GetOdds().ToList();
        var evens = tags.GetEvens().ToList();

        for (var i = 0; i < evens.Count; i++)
        {
            var key = keys[(int)evens[i]];
            var val = values[(int)odds[i]];
            var valObject = GetAttr(val);
            result.Add(new KeyValuePair<string, object>(key, valObject));
        }
        return result;
    }

    private static object GetAttr(Tile.Value value)
    {
        if (value.HasBoolValue)
        {
            return value.BoolValue;
        }
        else if (value.HasDoubleValue)
        {
            return value.DoubleValue;
        }
        else if (value.HasFloatValue)
        {
            return value.FloatValue;
        }
        else if (value.HasIntValue)
        {
            return value.IntValue;
        }
        else if (value.HasStringValue)
        {
            return value.StringValue;
        }
        else if (value.HasSIntValue)
        {
            return value.SintValue;
        }
        else if (value.HasUIntValue)
        {
            return value.UintValue;
        }
        else
        {
            throw new NotImplementedException("Unknown attribute type");
        }
    }
}
