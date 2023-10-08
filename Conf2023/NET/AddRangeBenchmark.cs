using BenchmarkDotNet.Attributes;
using Conf2023.EFmodel;

namespace Conf2023.NET;
[MemoryDiagnoser(false)]
public class AddRangeBenchmark
{
    [Params(100, 1000, 10000, 100000)]
    public int N;

    private List<int> _list;
    private IEnumerable<int> _itemsToAdd;


    [IterationSetup]
    public void GlobalSetup()
    {
        _list = new ();
        var items = new List<int>();
        for (int i = 0; i < N; i++)
            items.Add(i);
        _itemsToAdd = items.ToArray();
    }

    [Benchmark]
    public void AddRange()
    {
        _list.AddRange(_itemsToAdd);
    }
}

/* .NET 6.0
| Method   | N      | Mean      | Error     | StdDev    | Median    | Allocated |
|--------- |------- |----------:|----------:|----------:|----------:|----------:|
| AddRange | 100    |  1.446 us | 0.0845 us | 0.2492 us |  1.400 us |   1.04 KB |
| AddRange | 1000   |  1.464 us | 0.0702 us | 0.2015 us |  1.400 us |   4.55 KB |
| AddRange | 10000  |  2.998 us | 0.1056 us | 0.2980 us |  2.900 us |  39.71 KB |
| AddRange | 100000 | 17.348 us | 0.8760 us | 2.3979 us | 16.400 us | 391.27 KB |
 */

/* .NET 8.0
| Method   | N      | Mean      | Error     | StdDev    | Median    | Allocated |
|--------- |------- |----------:|----------:|----------:|----------:|----------:|
| AddRange | 100    |  1.609 us | 0.0367 us | 0.1053 us |  1.600 us |     824 B |
| AddRange | 1000   |  1.757 us | 0.0519 us | 0.1454 us |  1.700 us |    4424 B |
| AddRange | 10000  |  3.131 us | 0.1090 us | 0.3058 us |  3.000 us |   40424 B |
| AddRange | 100000 | 21.296 us | 1.5648 us | 4.3361 us | 20.100 us |  400424 B |
 */