using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Mapbox.Vector.Tile;
using System.IO;

namespace VelloAvaloniaSample
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

    // Custom canvas control that renders vector tiles using Avalonia's native drawing
    public class VectorTileCanvas : Control
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

        public override void Render(DrawingContext context)
        {
            base.Render(context);
            
            if (_tileData == null) return;

            // Draw white background
            context.FillRectangle(
                Brushes.White,
                new Rect(0, 0, Bounds.Width, Bounds.Height)
            );

            // Create a blue pen for drawing lines
            var pen = new Pen(Brushes.Blue, 2);

            // Render vector tile features
            foreach (var feature in _tileData.VectorTileFeatures)
            {
                if (feature.Geometry == null || feature.Geometry.Count == 0)
                    continue;

                var coords = feature.Geometry[0];
                for (var i = 1; i < coords.Count; i++)
                {
                    var c0 = coords[i - 1];
                    var c1 = coords[i];
                    
                    // Draw line using Avalonia's native drawing
                    context.DrawLine(
                        pen,
                        new Point(c0.X, c0.Y),
                        new Point(c1.X, c1.Y)
                    );
                }
            }
        }
    }
}
