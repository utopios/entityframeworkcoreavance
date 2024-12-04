using BenchmarkDotNet.Running;

namespace correction_tp_final.Benchmark;

public class MainBenchmark
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<Benchmarking>();
    }
}