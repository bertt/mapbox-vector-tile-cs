using System.Collections.Generic;
using System.Globalization;

namespace Mapbox.Vector.Tile;

public static class FeatureParser
{
    public static VectorTileFeature Parse(Tile.Feature feature, List<string> keys, List<Tile.Value> values, uint extent) => new(
        id: feature.Id.ToString(CultureInfo.InvariantCulture),
        geometry: GeometryParser.ParseGeometry(feature.Geometry, feature.Type),
        attributes: AttributesParser.Parse(keys, values, feature.Tags),
        geometryType: feature.Type,
        extent: extent
    );
}
