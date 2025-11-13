using System;
using System.Collections.Generic;

namespace Mapbox.Vector.Tile;

public static class AttributesEncoder
{
    public static List<uint> Encode(List<KeyValuePair<string, object>> attributes, List<string> keys, List<Tile.Value> values)
    {
        var tags = new List<uint>();

        foreach (var attribute in attributes)
        {
            var key = attribute.Key;
            var value = attribute.Value;

            // Get or add key index
            var keyIndex = keys.IndexOf(key);
            if (keyIndex == -1)
            {
                keyIndex = keys.Count;
                keys.Add(key);
            }

            // Get or add value index
            var tileValue = CreateTileValue(value);
            var valueIndex = FindValueIndex(values, tileValue);
            if (valueIndex == -1)
            {
                valueIndex = values.Count;
                values.Add(tileValue);
            }

            tags.Add((uint)keyIndex);
            tags.Add((uint)valueIndex);
        }

        return tags;
    }

    private static Tile.Value CreateTileValue(object value)
    {
        var tileValue = new Tile.Value();

        switch (value)
        {
            case string stringValue:
                tileValue.StringValue = stringValue;
                break;
            case bool boolValue:
                tileValue.BoolValue = boolValue;
                break;
            case float floatValue:
                tileValue.FloatValue = floatValue;
                break;
            case double doubleValue:
                tileValue.DoubleValue = doubleValue;
                break;
            case int intValue:
                tileValue.IntValue = intValue;
                break;
            case long longValue:
                tileValue.IntValue = longValue;
                break;
            case uint uintValue:
                tileValue.UintValue = uintValue;
                break;
            case ulong ulongValue:
                tileValue.UintValue = ulongValue;
                break;
            default:
                throw new NotImplementedException($"Unsupported attribute type: {value.GetType()}");
        }

        return tileValue;
    }

    private static int FindValueIndex(List<Tile.Value> values, Tile.Value newValue)
    {
        for (int i = 0; i < values.Count; i++)
        {
            var existingValue = values[i];
            if (ValuesEqual(existingValue, newValue))
            {
                return i;
            }
        }
        return -1;
    }

    private static bool ValuesEqual(Tile.Value a, Tile.Value b)
    {
        if (a.HasStringValue && b.HasStringValue)
            return a.StringValue == b.StringValue;
        if (a.HasBoolValue && b.HasBoolValue)
            return a.BoolValue == b.BoolValue;
        if (a.HasFloatValue && b.HasFloatValue)
            return Math.Abs(a.FloatValue - b.FloatValue) < 0.0001f;
        if (a.HasDoubleValue && b.HasDoubleValue)
            return Math.Abs(a.DoubleValue - b.DoubleValue) < 0.0001;
        if (a.HasIntValue && b.HasIntValue)
            return a.IntValue == b.IntValue;
        if (a.HasSIntValue && b.HasSIntValue)
            return a.SintValue == b.SintValue;
        if (a.HasUIntValue && b.HasUIntValue)
            return a.UintValue == b.UintValue;
        
        return false;
    }
}
