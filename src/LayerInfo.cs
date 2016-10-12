﻿using GeoJSON.Net.Feature;

namespace mapbox.vector.tile
{
    public class LayerInfo
    {
        public FeatureCollection FeatureCollection { get; set; }
        public string Name { get; set; }
		public uint Version { get; set; }
		public uint Extent { get; set; }
    }
}
