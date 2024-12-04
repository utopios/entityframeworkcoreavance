// See https://aka.ms/new-console-template for more information

using benchmark;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;

BenchmarkRunner.Run<LoadingBenchmark>();
