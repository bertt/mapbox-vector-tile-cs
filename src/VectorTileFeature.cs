using System.Collections.Generic;

namespace mapbox.vector.tile
{
	public class VectorTileFeature
	{
		public List<List<Coordinate>> Geometry {get;set;}
		public List<KeyValuePair<string, object>> Attributes { get; set; }
		public Tile.GeomType GeometryType { get; set; }
	}
}

