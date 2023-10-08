using BenchmarkDotNet.Attributes;

namespace Conf2023.NET;

[MemoryDiagnoser(false)]
public class ArraySkipTake
{
    string[] names =
    {
        "One",
        "Two",
        "Three",
        "Four",
        "Five",
        "Six",
        "Seven",
        "Eight",
        "Nine"
    };

    [Benchmark]
    public string[] Array()
    {
        return names.Skip(2).Take(3).ToArray();
    }

    [Benchmark]
    public ArraySegment<string> ArraySegment()
    {
        return new ArraySegment<string>(names, 2, 3);
    }

    [Benchmark]
    public Span<string> Span()
    {
        return names.AsSpan().Slice(2, 3);
    }
}


    /* .Net 6.0
    | Method       | Mean       | Error     | StdDev    | Allocated |
    |------------- |-----------:|----------:|----------:|----------:|
    | Array        | 61.8803 ns | 1.2318 ns | 1.5578 ns |     144 B |
    | ArraySegment |  1.5742 ns | 0.0445 ns | 0.0563 ns |         - |
    | Span         |  0.3015 ns | 0.0270 ns | 0.0266 ns |         - |
     */

    /* .Net 8.0
    | Method       | Mean       | Error     | StdDev    | Allocated |
    |------------- |-----------:|----------:|----------:|----------:|
    | Array        | 56.4334 ns | 1.0488 ns | 0.9298 ns |     144 B |
    | ArraySegment |  1.6112 ns | 0.0327 ns | 0.0306 ns |         - |
    | Span         |  0.3143 ns | 0.0316 ns | 0.0295 ns |         - |
     */