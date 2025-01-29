using BenchmarkDotNet.Running;
using System;

namespace mapbox.vector.tile.benchmark;

class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<ParsingBenchmark>();
        Console.ReadKey();
    }
}
