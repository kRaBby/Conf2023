using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Reports;
using System.Linq;

namespace Conf2023.NET;

[MemoryDiagnoser(false)]
public class ForVsForeachBenchmark
{
    [Params(100)]
    public int N;

    private int[] _array;
    private List<int> _list;
    private IList<int> _ilistArray;
    private IList<int> _ilistList;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _array = Enumerable.Range(1, N).ToArray();
        _list = _array.ToList();
        _ilistArray = _array;
        _ilistList = _list;
    }

    [Benchmark()]
    public void ForArray()
    {
        var sum = 0;
        for (var i = 0; i < _array.Length; ++i)
        {
            sum += _array[i];
        }
    }

    [Benchmark()]
    public void ForArrayOptimal()
    {
        var sum = 0;
        for (var i = 0; i < N; ++i)
        {
            sum += _array[i];
        }
    }

    [Benchmark()]
    public void ForArrayRevers()
    {
        var sum = 0;
        for (var i = N - 1; i >= 0; --i)
        {
            sum += _array[i];
        }
    }

    [Benchmark()]
    public void ForEachArray()
    {
        var sum = 0;
        foreach (var item in _array)
        {
            sum += item;
        }
    }

    [Benchmark()]
    public void ForList()
    {
        var sum = 0;
        for (var i = 0; i < N; ++i)
        {
            sum += _list[i];
        }
    }

    [Benchmark()]
    public void ForEachList()
    {
        var sum = 0;
        foreach (var item in _list)
        {
            sum += item;
        }
    }

    [Benchmark()]
    public void ForIListArray()
    {
        var sum = 0;
        for (var i = 0; i < N; ++i)
        {
            sum += _ilistArray[i];
        }
    }

    [Benchmark()]
    public void ForEachIListArray()
    {
        var sum = 0;
        foreach (var item in _ilistArray)
        {
            sum += item;
        }
    }

    [Benchmark()]
    public void ForIListList()
    {
        var sum = 0;
        for (var i = 0; i < N; ++i)
        {
            sum += _ilistList[i];
        }
    }

    [Benchmark()]
    public void ForEachIListList()
    {
        var sum = 0;
        foreach (var item in _ilistList)
        {
            sum += item;
        }
    }

    [Benchmark()]
    public void ForEachLINQList()
    {
        var sum = 0;
        _list.ForEach(x => { sum += x; });
    }
}

/*
| Method  | N      | Mean          | Error       | StdDev      | Allocated |
|-------- |------- |--------------:|------------:|------------:|----------:|
| For     | 10     |      3.880 ns |   0.0416 ns |   0.0389 ns |         - |
| ForEach | 10     |      2.568 ns |   0.0459 ns |   0.0384 ns |         - |
| For     | 100    |     48.551 ns |   0.9754 ns |   1.3673 ns |         - |
| ForEach | 100    |     33.495 ns |   0.6814 ns |   0.7573 ns |         - |
| For     | 1000   |    422.518 ns |   7.9323 ns |   6.6238 ns |         - |
| ForEach | 1000   |    264.539 ns |   5.1226 ns |   4.7916 ns |         - |
| For     | 100000 | 41,406.650 ns | 749.1511 ns | 700.7564 ns |         - |
| ForEach | 100000 | 26,272.374 ns | 285.7568 ns | 238.6199 ns |         - |


| Method            | N   | Mean      | Error    | StdDev   | Median    | Allocated |
|------------------ |---- |----------:|---------:|---------:|----------:|----------:|
| ForArray          | 100 |  52.12 ns | 0.316 ns | 0.247 ns |  52.10 ns |         - |
| ForArrayOptimal   | 100 |  44.43 ns | 0.875 ns | 1.168 ns |  44.49 ns |         - |
| ForEachArray      | 100 |  31.04 ns | 0.342 ns | 0.303 ns |  30.96 ns |         - |
| ForList           | 100 |  57.85 ns | 1.115 ns | 1.193 ns |  57.84 ns |         - |
| ForEachList       | 100 | 109.78 ns | 2.062 ns | 4.861 ns | 111.43 ns |         - |
| ForIListArray     | 100 | 221.83 ns | 2.234 ns | 1.980 ns | 221.64 ns |         - |
| ForEachIListArray | 100 | 410.56 ns | 8.206 ns | 8.780 ns | 409.49 ns |      32 B |
| ForIListList      | 100 | 224.22 ns | 2.293 ns | 2.145 ns | 225.01 ns |         - |
| ForEachIListList  | 100 | 514.40 ns | 8.222 ns | 8.075 ns | 515.36 ns |      40 B |

 */

    /* .Net 6.0
    | Method            | N   | Mean      | Error    | StdDev    | Median    | Allocated |
    |------------------ |---- |----------:|---------:|----------:|----------:|----------:|
    | ForArray          | 100 |  52.13 ns | 0.184 ns |  0.163 ns |  52.10 ns |         - |
    | ForArrayOptimal   | 100 |  43.39 ns | 0.391 ns |  0.346 ns |  43.31 ns |         - |
    | ForArrayRevers    | 100 |  43.56 ns | 0.197 ns |  0.164 ns |  43.59 ns |         - |
    | ForEachArray      | 100 |  29.98 ns | 0.101 ns |  0.090 ns |  29.99 ns |         - |
    | ForList           | 100 |  53.40 ns | 0.498 ns |  0.466 ns |  53.62 ns |         - |
    | ForEachList       | 100 |  77.77 ns | 0.428 ns |  0.400 ns |  77.69 ns |         - |
    | ForIListArray     | 100 | 196.79 ns | 0.633 ns |  0.592 ns | 196.95 ns |         - |
    | ForEachIListArray | 100 | 327.68 ns | 6.519 ns |  8.923 ns | 331.86 ns |      32 B |
    | ForIListList      | 100 | 163.56 ns | 3.516 ns | 10.312 ns | 158.64 ns |         - |
    | ForEachIListList  | 100 | 495.35 ns | 9.413 ns | 10.072 ns | 493.59 ns |      40 B |
    | ForEachLINQ       | 100 | 188.32 ns | 1.695 ns |  1.585 ns | 187.96 ns |      88 B |
     */

    /* .Net 8.0
    | Method            | N   | Mean      | Error    | StdDev   | Allocated |
    |------------------ |---- |----------:|---------:|---------:|----------:|
    | ForArray          | 100 |  52.62 ns | 0.317 ns | 0.281 ns |         - |
    | ForArrayOptimal   | 100 |  39.85 ns | 0.191 ns | 0.149 ns |         - |
    | ForArrayRevers    | 100 |  35.04 ns | 0.231 ns | 0.216 ns |         - |
    | ForEachArray      | 100 |  28.99 ns | 0.368 ns | 0.344 ns |         - |
    | ForList           | 100 |  54.13 ns | 1.031 ns | 0.965 ns |         - |
    | ForEachList       | 100 |  54.13 ns | 0.269 ns | 0.251 ns |         - |
    | ForIListArray     | 100 | 178.67 ns | 1.390 ns | 1.301 ns |         - |
    | ForEachIListArray | 100 | 103.52 ns | 0.711 ns | 0.555 ns |      32 B |
    | ForIListList      | 100 |  84.55 ns | 0.655 ns | 0.581 ns |         - |
    | ForEachIListList  | 100 | 139.47 ns | 1.345 ns | 1.123 ns |      40 B |
    | ForEachLINQ       | 100 |  93.43 ns | 0.591 ns | 0.493 ns |      88 B |
     */