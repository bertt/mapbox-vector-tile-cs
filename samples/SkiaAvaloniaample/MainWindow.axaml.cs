using Avalonia;
using Avalonia.Controls;
using Mapbox.Vector.Tile;
using System;
using System.IO;
using VelloSharp.Avalonia.Controls;

namespace SkiaAvaloniaample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // Create a simple VectorTileCanvas for rendering
            var velloControl = new VectorTileCanvas();
            SkiaPanel.Children.Add(velloControl);
        }
    }

    // Custom canvas control that renders vector tiles using VelloSharp
    public class VectorTileCanvas : VelloCanvasControl
    {
        private VectorTileLayer? _tileData;

        public VectorTileCanvas()
        {
            LoadTileData();
        }

        private void LoadTileData()
        {
            const string vtfile = @"cadastral.pbf";
            if (File.Exists(vtfile))
            {
                using var stream = File.OpenRead(vtfile);
                var layerInfos = VectorTileParser.Parse(stream);
                if (layerInfos.Count > 0)
                {
                    _tileData = layerInfos[0];
                }
            }
        }

        // Note: The exact rendering API will depend on VelloSharp's exposed methods
        // This is a placeholder that demonstrates the integration structure
        // The actual implementation would use VelloSharp's Scene-based rendering
        public void RenderVectorTile()
        {
            if (_tileData == null) return;

            // Render vector tile features using VelloSharp
            // This would use VelloSharp's Scene API once the correct method signatures are determined
            foreach (var feature in _tileData.VectorTileFeatures)
            {
                var coords = feature.Geometry[0];
                for (var i = 1; i < coords.Count; i++)
                {
                    var c0 = coords[i - 1];
                    var c1 = coords[i];
                    
                    // Draw lines - exact API TBD based on VelloSharp documentation
                }
            }
        }
    }
}
