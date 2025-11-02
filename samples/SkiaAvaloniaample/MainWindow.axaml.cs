using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using Mapbox.Vector.Tile;
using SkiaSharp;
using System.IO;

namespace SkiaAvaloniaample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // Create a custom control for rendering
            var skiaControl = new SkiaRenderControl();
            SkiaPanel.Children.Add(skiaControl);
        }
    }

    public class SkiaRenderControl : Control
    {
        private class CustomDrawOp : ICustomDrawOperation
        {
            public void Dispose()
            {
            }

            public Rect Bounds { get; set; }

            public bool HitTest(Point p) => false;

            public bool Equals(ICustomDrawOperation? other) => false;

            public void Render(ImmediateDrawingContext context)
            {
                var leaseFeature = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
                if (leaseFeature == null)
                    return;

                using var lease = leaseFeature.Lease();
                var canvas = lease.SkCanvas;

                // Clear the canvas with a white background
                canvas.Clear(SKColors.White);

                // Create a paint for drawing
                using var paint = new SKPaint
                {
                    Color = SKColors.Blue,
                    StrokeWidth = 5,
                    IsAntialias = true
                };

                const string vtfile = @"cadastral.pbf";
                if (File.Exists(vtfile))
                {
                    using var stream = File.OpenRead(vtfile);
                    var layerInfos = VectorTileParser.Parse(stream);
                    if (layerInfos.Count > 0)
                    {
                        var layerInfo = layerInfos[0];
                        foreach (var feature in layerInfo.VectorTileFeatures)
                        {
                            var coords = feature.Geometry[0];
                            for (var i = 1; i < coords.Count; i++)
                            {
                                var c0 = coords[i - 1];
                                var c1 = coords[i];
                                canvas.DrawLine(c0.X, c0.Y, c1.X, c1.Y, paint);
                            }
                        }
                    }
                }
            }
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);
            context.Custom(new CustomDrawOp { Bounds = new Rect(0, 0, Bounds.Width, Bounds.Height) });
        }
    }
}
