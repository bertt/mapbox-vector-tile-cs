# SkiaAvaloniaample

An Avalonia-based sample application that demonstrates rendering Mapbox Vector Tiles using VelloSharp.

## Description

This cross-platform desktop application uses Avalonia UI framework with VelloSharp to visualize vector tile data from a `.pbf` (Protobuf) file. It reads cadastral data and renders the geometries using Vello's GPU-accelerated rendering engine.

## Features

- Cross-platform support (Windows, macOS, Linux) via Avalonia
- VelloSharp for high-performance GPU-accelerated 2D graphics rendering
- Reads and parses Mapbox Vector Tile format (.pbf files)
- Visualizes vector tile geometries

## Requirements

- .NET 8.0 or later
- Avalonia 11.3.6
- VelloSharp 0.5.0-alpha.3

## Running the Sample

```bash
cd samples/SkiaAvaloniaample
dotnet run
```

## Project Structure

- `Program.cs` - Application entry point
- `App.axaml` / `App.axaml.cs` - Avalonia application configuration
- `MainWindow.axaml` / `MainWindow.axaml.cs` - Main window with custom VelloSharp rendering
- `cadastral.pbf` - Sample vector tile data file

## How It Works

The application uses a custom `VectorTileCanvas` that extends VelloSharp's `VelloCanvasControl`. It:

1. Reads the vector tile data from `cadastral.pbf`
2. Parses the tile using `VectorTileParser.Parse()`
3. Renders each feature's geometry using Vello's scene-based rendering API

This sample demonstrates cross-platform vector tile rendering using VelloSharp, a .NET binding for the Linebender graphics stack (vello, wgpu, winit).
