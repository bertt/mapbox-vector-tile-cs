using BenchmarkDotNet.Running;
using System;

namespace mapbox.vector.tile.benchmark;

class Program
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run<ParsingBenchmark>();
        BenchmarkRunner.Run<BooleanAttributesBenchmark>();
        Console.ReadKey();
    }
}
