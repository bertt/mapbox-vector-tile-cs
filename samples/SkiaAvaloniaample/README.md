# SkiaAvaloniaample

An Avalonia-based sample application that demonstrates rendering Mapbox Vector Tiles using SkiaSharp.

## Description

This cross-platform desktop application uses Avalonia UI framework with SkiaSharp to visualize vector tile data from a `.pbf` (Protobuf) file. It reads cadastral data and renders the geometries as blue lines on a white canvas.

## Features

- Cross-platform support (Windows, macOS, Linux) via Avalonia
- SkiaSharp for high-performance 2D graphics rendering
- Reads and parses Mapbox Vector Tile format (.pbf files)
- Visualizes vector tile geometries

## Requirements

- .NET 8.0 or later
- Avalonia 11.0.10
- SkiaSharp 3.116.1

## Running the Sample

```bash
cd samples/SkiaAvaloniaample
dotnet run
```

## Project Structure

- `Program.cs` - Application entry point
- `App.axaml` / `App.axaml.cs` - Avalonia application configuration
- `MainWindow.axaml` / `MainWindow.axaml.cs` - Main window with custom SkiaSharp rendering
- `cadastral.pbf` - Sample vector tile data file

## How It Works

The application uses a custom `SkiaRenderControl` that implements Avalonia's custom drawing operations. It:

1. Reads the vector tile data from `cadastral.pbf`
2. Parses the tile using `VectorTileParser.Parse()`
3. Renders each feature's geometry as connected line segments using SkiaSharp canvas

This sample demonstrates the same functionality as the Windows Forms sample but with the advantage of being cross-platform.
