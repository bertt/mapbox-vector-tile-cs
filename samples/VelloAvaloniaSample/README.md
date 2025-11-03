# VelloAvaloniaSample

An Avalonia-based sample application that demonstrates rendering Mapbox Vector Tiles using VelloSharp (Vello GPU/CPU renderer).

## Description

This cross-platform desktop application uses VelloSharp's sparse renderer (CPU-based) to visualize vector tile data from a `.pbf` (Protobuf) file. It integrates the Vello rendering engine with Avalonia UI framework using custom draw operations, providing hardware-accelerated (or CPU fallback) 2D graphics rendering for cadastral data.

## Features

- Cross-platform support (Windows, macOS, Linux) via Avalonia
- VelloSharp sparse renderer for 2D graphics rendering
- CPU-based rendering (no GPU required)
- Direct integration with Avalonia's Skia backend via ICustomDrawOperation
- Reads and parses Mapbox Vector Tile format (.pbf files)
- Visualizes vector tile geometries
- Blue lines on white background rendering

## Requirements

- .NET 8.0 or later
- Avalonia 11.3.6
- VelloSharp 0.5.0-alpha.3 (with native libraries)
- SkiaSharp 2.88.9

## Running the Sample

```bash
cd samples/VelloAvaloniaSample
dotnet run
```

## Project Structure

- `Program.cs` - Application entry point
- `App.axaml` / `App.axaml.cs` - Avalonia application configuration
- `MainWindow.axaml` / `MainWindow.axaml.cs` - Main window with VelloSharp rendering
- `cadastral.pbf` - Sample vector tile data file

## How It Works

The application uses a custom `VelloRenderControl` that integrates VelloSharp's sparse renderer with Avalonia via custom draw operations. It:

1. Reads the vector tile data from `cadastral.pbf`
2. Parses the tile using `VectorTileParser.Parse()`
3. Uses Avalonia's `ICustomDrawOperation` to integrate with the rendering pipeline
4. VelloSharp's `SparseRenderContext` renders geometries to a byte buffer
5. The sparse renderer provides CPU-based rendering, eliminating the need for GPU acceleration
6. The buffer is converted to an SKBitmap and drawn directly to the Skia canvas
7. No WriteableBitmap is used - rendering goes directly to the native Skia surface

This sample demonstrates cross-platform vector tile rendering using the Vello rendering engine integrated with Avalonia UI's Skia backend through custom draw operations.
