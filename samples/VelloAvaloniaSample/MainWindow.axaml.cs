using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using Mapbox.Vector.Tile;
using SkiaSharp;
using System;
using System.IO;
using System.Numerics;
using VelloSharp;

namespace VelloAvaloniaSample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // Create a VelloRenderControl for rendering with Vello
            var velloControl = new VelloRenderControl();
            SkiaPanel.Children.Add(velloControl);
        }
    }

    // Custom control that renders vector tiles using VelloSharp
    public class VelloRenderControl : Control
    {
        private VectorTileLayer? _tileData;

        public VelloRenderControl()
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

            var width = (int)Bounds.Width;
            var height = (int)Bounds.Height;

            if (width <= 0 || height <= 0 || _tileData == null)
                return;

            // Use custom draw operation to render with VelloSharp
            context.Custom(new VelloDrawOperation(new Rect(0, 0, width, height), _tileData));
        }
    }

    // Custom draw operation that uses VelloSharp to render
    public class VelloDrawOperation : ICustomDrawOperation
    {
        private readonly Rect _bounds;
        private readonly VectorTileLayer _tileData;

        public VelloDrawOperation(Rect bounds, VectorTileLayer tileData)
        {
            _bounds = bounds;
            _tileData = tileData;
        }

        public Rect Bounds => _bounds;

        public void Dispose()
        {
            // Nothing to dispose
        }

        public bool Equals(ICustomDrawOperation? other)
        {
            return other is VelloDrawOperation op && op._bounds == _bounds;
        }

        public bool HitTest(Point p)
        {
            return _bounds.Contains(p);
        }

        public void Render(ImmediateDrawingContext context)
        {
            var leaseFeature = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            if (leaseFeature == null)
                return;

            using var lease = leaseFeature.Lease();
            if (lease == null)
                return;

            // Use reflection to get the canvas property (name varies by version)
            var canvasProperty = lease.GetType().GetProperty("SkiaCanvas") ?? lease.GetType().GetProperty("Canvas");
            var canvas = canvasProperty?.GetValue(lease) as SKCanvas;
            
            if (canvas == null)
                return;

            var width = (int)_bounds.Width;
            var height = (int)_bounds.Height;

            // Clamp dimensions to ushort range for SparseRenderContext
            var clampedWidth = (ushort)Math.Min(width, ushort.MaxValue);
            var clampedHeight = (ushort)Math.Min(height, ushort.MaxValue);

            var options = new SparseRenderContextOptions
            {
                EnableMultithreading = true
            };

            using var renderer = new SparseRenderContext(clampedWidth, clampedHeight, options);

            // Clear the renderer
            renderer.Reset();

            // Fill background with white
            renderer.FillRect(0, 0, width, height, new RgbaColor(255, 255, 255, 255));

            // Draw vector tile features using VelloSharp
            var strokeStyle = new StrokeStyle
            {
                Width = 2.0f
            };

            foreach (var feature in _tileData.VectorTileFeatures)
            {
                if (feature.Geometry == null || feature.Geometry.Count == 0)
                    continue;

                var coords = feature.Geometry[0];
                for (var i = 1; i < coords.Count; i++)
                {
                    var c0 = coords[i - 1];
                    var c1 = coords[i];

                    var linePath = new PathBuilder();
                    linePath.MoveTo(c0.X, c0.Y);
                    linePath.LineTo(c1.X, c1.Y);

                    renderer.StrokePath(
                        linePath,
                        strokeStyle,
                        Matrix3x2.Identity,
                        new RgbaColor(0, 0, 255, 255)
                    );
                }
            }

            // Render to a byte buffer
            var imageInfo = new SKImageInfo(width, height, SKColorType.Bgra8888, SKAlphaType.Premul);
            var buffer = new byte[imageInfo.RowBytes * height];
            
            unsafe
            {
                fixed (byte* ptr = buffer)
                {
                    var bufferSpan = new Span<byte>(ptr, buffer.Length);
                    renderer.RenderTo(bufferSpan, SparseRenderMode.OptimizeSpeed);
                }
            }

            // Create SKBitmap from buffer and draw to canvas
            unsafe
            {
                fixed (byte* ptr = buffer)
                {
                    using var skBitmap = new SKBitmap();
                    skBitmap.InstallPixels(imageInfo, (IntPtr)ptr, imageInfo.RowBytes);
                    canvas.DrawBitmap(skBitmap, 0, 0);
                }
            }
        }
    }
}
