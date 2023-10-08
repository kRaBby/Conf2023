using BenchmarkDotNet.Attributes;

namespace Conf2023.NET;
[MemoryDiagnoser(false)]
public class AnyVsExistsBenchmark
{
    private int N = 100;
    private int Found = 50;
    private int NotFound = 101;

    private List<int> _list;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _list = new List<int>();
        for (int i = 0; i < N; i++)
        {
            _list.Add(i);
        }
    }

    [Benchmark()]
    public void AnyFound()
    {
        _list.Any(x => x == Found);
    }

    [Benchmark()]
    public void AnyNotFound()
    {
        _list.Any(x => x == NotFound);
    }

    [Benchmark()]
    public void ExistsFound()
    {
        _list.Exists(x => x == Found);
    }

    [Benchmark()]
    public void ExistsNotFound()
    {
        _list.Exists(x => x == NotFound);
    }
}

    /* .Net 6.0
    | Method         | Mean      | Error     | StdDev    | Allocated |
    |--------------- |----------:|----------:|----------:|----------:|
    | AnyFound       | 343.51 ns |  6.592 ns |  6.166 ns |     104 B |
    | AnyNotFound    | 695.07 ns | 13.741 ns | 25.810 ns |     104 B |
    | ExistsFound    |  90.85 ns |  0.752 ns |  0.666 ns |      64 B |
    | ExistsNotFound | 171.79 ns |  2.621 ns |  2.452 ns |      64 B |
     */

    /* .Net 8.0
    | Method         | Mean      | Error    | StdDev   | Allocated |
    |--------------- |----------:|---------:|---------:|----------:|
    | AnyFound       | 101.33 ns | 0.383 ns | 0.359 ns |     104 B |
    | AnyNotFound    | 185.81 ns | 1.277 ns | 1.132 ns |     104 B |
    | ExistsFound    |  54.09 ns | 0.463 ns | 0.410 ns |      64 B |
    | ExistsNotFound |  91.23 ns | 0.594 ns | 0.556 ns |      64 B |
     */