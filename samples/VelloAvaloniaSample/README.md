# VelloAvaloniaSample

An Avalonia-based sample application that demonstrates rendering Mapbox Vector Tiles using Avalonia's native drawing APIs.

## Description

This cross-platform desktop application uses Avalonia UI framework with its native 2D graphics rendering to visualize vector tile data from a `.pbf` (Protobuf) file. It reads cadastral data and renders the geometries as blue lines on a white canvas.

## Features

- Cross-platform support (Windows, macOS, Linux) via Avalonia
- Avalonia's native DrawingContext for 2D graphics rendering
- Reads and parses Mapbox Vector Tile format (.pbf files)
- Visualizes vector tile geometries

## Requirements

- .NET 8.0 or later
- Avalonia 11.3.6

## Running the Sample

```bash
cd samples/VelloAvaloniaSample
dotnet run
```

## Project Structure

- `Program.cs` - Application entry point
- `App.axaml` / `App.axaml.cs` - Avalonia application configuration
- `MainWindow.axaml` / `MainWindow.axaml.cs` - Main window with custom rendering
- `cadastral.pbf` - Sample vector tile data file

## How It Works

The application uses a custom `VectorTileCanvas` that extends Avalonia's `Control` class. It:

1. Reads the vector tile data from `cadastral.pbf`
2. Parses the tile using `VectorTileParser.Parse()`
3. Overrides the `Render` method to draw geometries using Avalonia's native `DrawingContext`

This sample demonstrates cross-platform vector tile rendering using Avalonia's built-in 2D drawing capabilities.
