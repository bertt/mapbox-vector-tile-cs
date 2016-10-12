using System;
using System.Collections;
using System.Collections.Generic;

namespace mapbox.vector.tile
{
	public class VectorTileLayer
	{
		public VectorTileLayer()
		{
		}

		public List<VectorTileFeature> VectorTileFeatures { get;set; }
		public string Name { get; set; }
		public uint Version { get; set; }
		public uint Extent { get; set; }
	}
}

