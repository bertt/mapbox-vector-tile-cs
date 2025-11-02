using Mapbox.Vector.Tile;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace VelloWindowsFormSample
{
    public partial class Form1 : Form
    {
        private SKControl skControl;
        public Form1()
        {
            InitializeComponent();
            skControl = new SKControl
            {
                Dock = DockStyle.Fill // Laat het de hele Form vullen
            };
            skControl.PaintSurface += OnPaintSurface;
            Controls.Add(skControl);
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            // Verkrijg het canvas
            SKCanvas canvas = e.Surface.Canvas;

            // Wis het canvas met een achtergrondkleur
            canvas.Clear(SKColors.White);

            // Maak een pen (SKPaint) aan
            var paint = new SKPaint
            {
                Color = SKColors.Blue,
                StrokeWidth = 5,
                IsAntialias = true
            };

            const string vtfile = @"cadastral.pbf";
            var stream = File.OpenRead(vtfile);
            var layerInfos = VectorTileParser.Parse(stream);
            var laerInfo = layerInfos[0];
            foreach (var feature in laerInfo.VectorTileFeatures)
            {
                var coords = feature.Geometry[0];
                for (var i = 1; i < coords.Count; i++)
                {
                    var c0 = coords[i-1];
                    var c1 = coords[i];
                    canvas.DrawLine(c0.X, c0.Y, c1.X, c1.Y, paint);
                }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
