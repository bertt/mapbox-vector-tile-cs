# mapbox-vector-tile-cs 

[![NuGet Status](http://img.shields.io/nuget/v/mapbox-vector-tile.svg?style=flat)](https://www.nuget.org/packages/mapbox-vector-tile/) ![.NET 8](https://github.com/bertt/mapbox-vector-tile-cs/workflows/.NET%208/badge.svg)

.NET Standard 2.0 library for decoding a Mapbox vector tile. 

## Dependencies

- protobuf-net https://github.com/protobuf-net/protobuf-net

## Installation

```
$ Install-Package mapbox-vector-tile
```

## Usage

```cs
const string vtfile = "vectortile.pbf";
var stream = File.OpenRead(vtfile);
var layerInfos = VectorTileParser.Parse(stream);
```

Tip: If you use this library with vector tiles loading from a webserver, you could run into the following exception: 
'ProtoBuf.ProtoException: Invalid wire-type; this usually means you have over-written a file without truncating or setting the length'
Probably you need to check the GZip compression, see also TileParserTests.cs for an example.

## Building

```
$ git clone https://github.com/bertt/mapbox-vector-tile-cs.git
$ cd mapbox-vector-tile-cs
$ dotnet build
```

## Testing

```
$ git clone https://github.com/bertt/mapbox-vector-tile-cs.git
$ cd mapbox-vector-tile-cs/tests/mapbox.vector.tile.tests
$ dotnet test
Passed!
Failed: 0, Passed: 38, Skipped: 0, Total: 38, Duration: 937 ms
```

## Samples

1] GeoJSON

The samples folder contains a simple console application that reads a vector tile from a file and converts to GeoJSON file.

2] SkiaSharp Windows Forms Sample

SkiaSharp is a cross-platform 2D graphics API for .NET platforms based on Google's Skia Graphics Library. The samples folder contains a simple Windows Forms GUI application that reads a 
vector tile from a file and draws the geometries using SkiaSharp.

3] Avalonia Sample

The samples folder contains a cross-platform GUI application built with Avalonia UI that reads a vector tile from a file and draws the geometries using Avalonia's native DrawingContext API. This sample works on Windows, macOS, and Linux.

## Benchmarking

Test performed with Mapbox vector tile '14-8801-5371.vector.pbf'

Layers used:

Point layer: parks (id=17) - 558 features

Line layer: roads (id=8) - 686 features

Polygon layer: building (id=5) - 975 features

```

| Method                    | Mean     | Error     | StdDev    |
|-------------------------- |---------:|----------:|----------:|
| ParseVectorTileFromStream | 1.401 us | 0.0133 us | 0.0125 us |
```

## History

2025-04-16: Release 5.2.3, updated licence in Nuget package

2025-02-25: Release 5.2.2, nullable enabled

2025-02-24: Release 5.2.1, improved memory handling

2025-02-24: Release 5.2, library is now .NET Standard 2.0

2025-01-29: Release 5.1, upgrading dependencies + remove GeoJSON.NET dependency + adding samples (SkiaSharp, GeoJSON)

2023-11-26: Release 5.0.2, containing .NET 8

2022-10-29: Release 5.0.1, upgrading dependencies 

2022-10-29: Release 5.0 containing .NET 6

2018-08-28: Release 4.2 containing .NET Standard 2.0

2018-03-08: Release 4.1 with fix for issue 16 (https://github.com/bertt/mapbox-vector-tile-cs/issues/16 - about serializing attributes)

2017-10-19: Release 4.0 for .NET Standard 1.3

2016-11-03: Release 3.1

Changes: Add support for polygon inner- and outerrings

2016-10-31: Release 3.0

Changes: Add support for multi-geometries 

2015-07-08: Release 2.0 

2015-07-07: Release 1.0 


