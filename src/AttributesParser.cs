using System.Collections.Generic;
using System.Linq;
using mapbox.vector.tile.ExtensionMethods;

namespace mapbox.vector.tile
{
    public class AttributesParser
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
