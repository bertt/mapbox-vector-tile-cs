using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Mapbox.Vector.Tile;
using System;
using System.IO;
using System.Numerics;
using VelloSharp;
using VelloFillRule = VelloSharp.FillRule;
using AvaloniaVector = Avalonia.Vector;

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
        private WriteableBitmap? _bitmap;
        private Renderer? _renderer;
        private Scene? _scene;
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

            if (width <= 0 || height <= 0)
                return;

            // Initialize or resize renderer if needed
            if (_renderer == null || _lastWidth != width || _lastHeight != height)
            {
                _renderer?.Dispose();
                _scene?.Dispose();
                
                _renderer = new Renderer((uint)width, (uint)height);
                _scene = new Scene();
                _lastWidth = width;
                _lastHeight = height;
                
                // Create a new WriteableBitmap
                _bitmap = new WriteableBitmap(
                    new PixelSize(width, height),
                    new AvaloniaVector(96, 96),
                    PixelFormat.Bgra8888,
                    AlphaFormat.Premul);
            }

            if (_scene != null && _bitmap != null && _renderer != null)
            {
                // Clear the scene
                _scene.Reset();

                // Fill background with white
                var pathBuilder = new PathBuilder();
                pathBuilder.MoveTo(0, 0);
                pathBuilder.LineTo(width, 0);
                pathBuilder.LineTo(width, height);
                pathBuilder.LineTo(0, height);
                pathBuilder.Close();

                _scene.FillPath(
                    pathBuilder,
                    VelloFillRule.NonZero,
                    Matrix3x2.Identity,
                    new RgbaColor(255, 255, 255, 255)
                );

                // Draw vector tile features using VelloSharp
                if (_tileData != null)
                {
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

                            var strokeStyle = new StrokeStyle
                            {
                                Width = 2.0f
                            };

                            _scene.StrokePath(
                                linePath,
                                strokeStyle,
                                Matrix3x2.Identity,
                                new RgbaColor(0, 0, 255, 255)
                            );
                        }
                    }
                }

                // Render the scene to a byte buffer
                using (var frameBuffer = _bitmap.Lock())
                {
                    unsafe
                    {
                        var renderParams = new RenderParams();
                        var bufferSpan = new Span<byte>(
                            (void*)frameBuffer.Address,
                            frameBuffer.RowBytes * height
                        );
                        
                        _renderer.Render(_scene, renderParams, bufferSpan, frameBuffer.RowBytes);
                    }
                }

                // Draw the bitmap to the Avalonia DrawingContext
                context.DrawImage(_bitmap, new Rect(0, 0, width, height));
            }
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromVisualTree(e);
            
            // Clean up resources
            _renderer?.Dispose();
            _scene?.Dispose();
            _bitmap?.Dispose();
        }
    }
}
