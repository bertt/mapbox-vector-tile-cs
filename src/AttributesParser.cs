using System;
using System.Collections.Generic;
using System.Linq;
using Mapbox.Vectors.ExtensionMethods;
using Mapbox.Vectors.mapnik.vector;

namespace Mapbox.Vectors
{
    public class AttributesParser
    {
        public static List<KeyValuePair<String, Object>> Parse(List<string> keys, List<tile.value> values, List<uint> tags)
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

        private static object GetAttr(tile.value value)
        {
            object res = null;

            if (value.HasBoolValue)
            {
                res = value.bool_value;
            }
            else if (value.HasDoubleValue)
            {
                res = value.double_value;
            }
            else if (value.HasFloatValue)
            {
                res = value.float_value;
            }
            else if (value.HasIntValue)
            {
                res = value.int_value;
            }
            else if (value.HasStringValue)
            {
                res = value.string_value;
            }
            else if (value.HasUIntValue)
            {
                res = value.HasUIntValue;
            }
            return res;
        }

    }
}
