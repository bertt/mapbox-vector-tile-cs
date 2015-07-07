using System;
using System.Collections.Generic;
using System.Linq;
using Mapbox.Vectors.ExtensionMethods;

namespace mapbox.vector.tile
{
    public class AttributesParser
    {
        public static List<KeyValuePair<String, Object>> Parse(List<string> keys, List<Tile.Value> values, List<uint> tags)
        {
            var result = new List<KeyValuePair<String, Object>>();
            var odds = tags.GetOdds();
            var evens = tags.GetEvens();

            for (var i = 0; i < evens.ToList().Count; i++)
            {
                var key = keys[(int)evens.ToList()[i]];
                var val = values[(int)odds.ToList()[i]];
                var valObject = GetAttr(val);
                result.Add(new KeyValuePair<string, object>(key, valObject));
            }
            return result;
        }

        private static object GetAttr(Tile.Value value)
        {
            object res = null;

            if (value.HasBoolValue)
            {
                res = value.BoolValue;
            }
            else if (value.HasDoubleValue)
            {
                res = value.DoubleValue;
            }
            else if (value.HasFloatValue)
            {
                res = value.FloatValue;
            }
            else if (value.HasIntValue)
            {
                res = value.IntValue;
            }
            else if (value.HasStringValue)
            {
                res = value.StringValue;
            }
            else if (value.HasUIntValue)
            {
                res = value.HasUIntValue;
            }
            return res;
        }
    }
}
