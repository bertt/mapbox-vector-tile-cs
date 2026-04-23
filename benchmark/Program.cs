using BenchmarkDotNet.Running;
using System;

namespace mapbox.vector.tile.benchmark;

class Program
{
    static void Main(string[] args)
    {
        Type benchmarkType;
        if (args.Length == 0)
        {
            benchmarkType = typeof(ParsingBenchmark);
        }
        else
        {
            var fullName = "mapbox.vector.tile.benchmark." + args[0];
            benchmarkType = Type.GetType(fullName) ?? Type.GetType(fullName + "Benchmark") ?? throw new ArgumentException($"Unrecognised type '{args[0]}'");
        }

        var summary = BenchmarkRunner.Run(benchmarkType);
        Console.ReadKey();
    }
}
