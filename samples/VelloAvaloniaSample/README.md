# VelloAvaloniaSample

An Avalonia-based sample application that demonstrates rendering Mapbox Vector Tiles using VelloSharp (Vello GPU/CPU renderer).

## Description

This cross-platform desktop application uses VelloSharp's sparse renderer (CPU-based) to visualize vector tile data from a `.pbf` (Protobuf) file. It integrates the Vello rendering engine with Avalonia UI framework, providing hardware-accelerated (or CPU fallback) 2D graphics rendering for cadastral data.

## Features

- Cross-platform support (Windows, macOS, Linux) via Avalonia
- VelloSharp sparse renderer for 2D graphics rendering
- CPU-based rendering (no GPU required)
- Direct buffer-to-bitmap rendering using Avalonia's native bitmap APIs
- Reads and parses Mapbox Vector Tile format (.pbf files)
- Visualizes vector tile geometries
- Blue lines on white background rendering

## Requirements

- .NET 8.0 or later
- Avalonia 11.3.6
- VelloSharp 0.5.0-alpha.3 (with native libraries)

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

The application uses a custom `VelloRenderControl` that integrates VelloSharp's sparse renderer with Avalonia. It:

1. Reads the vector tile data from `cadastral.pbf`
2. Parses the tile using `VectorTileParser.Parse()`
3. Uses VelloSharp's `SparseRenderContext` to render geometries to a byte buffer
4. The sparse renderer provides CPU-based rendering, eliminating the need for GPU acceleration
5. Creates an Avalonia `Bitmap` directly from the buffer using native APIs
6. Renders the bitmap to the screen using Avalonia's `DrawingContext`

This sample demonstrates cross-platform vector tile rendering using the Vello rendering engine integrated with Avalonia UI, without requiring SkiaSharp or reflection.
