using Mapbox.Vector.Tile;
using VelloSharp.WinForms;
using VelloSharp.WinForms.Integration;
using System.Drawing;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private VelloRenderControl velloControl;
        public Form1()
        {
            InitializeComponent();
            velloControl = new VelloRenderControl
            {
                Dock = DockStyle.Fill // Laat het de hele Form vullen
            };
            velloControl.PaintSurface += OnPaintSurface;
            Controls.Add(velloControl);
        }

        private void OnPaintSurface(object sender, VelloPaintSurfaceEventArgs e)
        {
            // Verkrijg de graphics context
            var graphics = e.GetGraphics();

            // Wis het canvas met een achtergrondkleur (via clear color op scene)
            graphics.Clear(Color.White);

            // Maak een pen aan
            var pen = new VelloPen(Color.Blue, 5);

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
                    graphics.DrawLine(pen, new PointF(c0.X, c0.Y), new PointF(c1.X, c1.Y));
                }

            }

            // Dispose de pen
            pen.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
