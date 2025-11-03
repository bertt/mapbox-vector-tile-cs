using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Mapbox.Vector.Tile;
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
        private SparseRenderContext? _renderer;
        private byte[]? _renderBuffer;
        private int _lastWidth;
        private int _lastHeight;

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

            // Initialize or resize renderer if needed
            if (_renderer == null || _lastWidth != width || _lastHeight != height)
            {
                _renderer?.Dispose();
                
                // Clamp dimensions to ushort range for SparseRenderContext
                var clampedWidth = (ushort)Math.Min(width, ushort.MaxValue);
                var clampedHeight = (ushort)Math.Min(height, ushort.MaxValue);
                
                var options = new SparseRenderContextOptions
                {
                    EnableMultithreading = true
                };
                
                _renderer = new SparseRenderContext(clampedWidth, clampedHeight, options);
                _lastWidth = width;
                _lastHeight = height;
                
                // Calculate buffer size (BGRA8888 = 4 bytes per pixel)
                _renderBuffer = new byte[width * height * 4];
            }

            if (_renderer != null && _renderBuffer != null)
            {
                // Clear the renderer
                _renderer.Reset();

                // Fill background with white using VelloSharp
                _renderer.FillRect(0, 0, width, height, new RgbaColor(255, 255, 255, 255));

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

                        _renderer.StrokePath(
                            linePath,
                            strokeStyle,
                            Matrix3x2.Identity,
                            new RgbaColor(0, 0, 255, 255)
                        );
                    }
                }

                // Render to buffer using VelloSharp
                unsafe
                {
                    fixed (byte* ptr = _renderBuffer)
                    {
                        var bufferSpan = new Span<byte>(ptr, _renderBuffer.Length);
                        _renderer.RenderTo(bufferSpan, SparseRenderMode.OptimizeSpeed);
                    }
                }

                // Convert buffer to Avalonia bitmap and draw
                unsafe
                {
                    fixed (byte* ptr = _renderBuffer)
                    {
                        using var bitmap = new Avalonia.Media.Imaging.Bitmap(
                            Avalonia.Platform.PixelFormat.Bgra8888,
                            Avalonia.Platform.AlphaFormat.Premul,
                            (IntPtr)ptr,
                            new Avalonia.PixelSize(width, height),
                            new Avalonia.Vector(96, 96),
                            width * 4);
                        
                        context.DrawImage(bitmap, new Rect(0, 0, width, height));
                    }
                }
            }
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromVisualTree(e);
            
            // Clean up resources
            _renderer?.Dispose();
        }
    }
}
