using BenchmarkDotNet.Attributes;
using Conf2023.EFmodel;
using Microsoft.EntityFrameworkCore;

namespace Conf2023.EF;

[MemoryDiagnoser(false)]
public class QueryBenchmark
{
    private AppDbContext _appDbContext;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _appDbContext = new AppDbContext();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        _appDbContext.Dispose();
    }

    [Benchmark]
    public void BaseQuery()
    {
        var result = _appDbContext.Books
            .Include(x => x.Author)
            .ToArray()
            .OrderBy(x => x.Year)
            .Where(x => x.Id == 5)
            .Where(x => x.Author.Id == 10)
            .ToArray();

        var sum = 0;
        foreach (var item in result)
        {
            sum += item.Year;
        }
    }

    [Benchmark]
    public void MaterializeOptimization()
    {
        var result = _appDbContext.Books
            .OrderBy(x => x.Year)
            .Where(x => x.Id == 5)
            .Where(x => x.Author.Id == 10)
            .ToArray();

        var sum = 0;
        foreach (var item in result)
        {
            sum += item.Year;
        }
    }

    [Benchmark]
    public void WhereOptimization()
    {
        var result = _appDbContext.Books
            .OrderBy(x => x.Year)
            .Where(x => x.Id == 5 && x.Author.Id == 10)
            .ToArray();

        var sum = 0;
        foreach (var item in result)
        {
            sum += item.Year;
        }
    }


    [Benchmark]
    public void OrderOptimization()
    {
        var result = _appDbContext.Books
            .Where(x => x.Id == 5 && x.Author.Id == 10)
            .OrderBy(x => x.Year)
            .ToArray();

        var sum = 0;
        foreach (var item in result)
        {
            sum += item.Year;
        }
    }

    [Benchmark]
    public void AsNoTrackingOptimization()
    {
        var result = _appDbContext.Books
            .AsNoTracking()
            .Where(x => x.Id == 5 && x.Author.Id == 10)
            .OrderBy(x => x.Year)
            .ToArray();

        var sum = 0;
        foreach (var item in result)
        {
            sum += item.Year;
        }
    }

    [Benchmark]
    public async Task AsyncOptimization()
    {
        var result = await _appDbContext.Books
            .AsNoTracking()
            .Where(x => x.Id == 5 && x.Author.Id == 10)
            .OrderBy(x => x.Year)
            .ToArrayAsync();

        var sum = 0;
        foreach (var item in result)
        {
            sum += item.Year;
        }
    }

}
    /* MsSql, .Net 8.0
    | Method                   | Mean       | Error    | StdDev   | Allocated |
    |------------------------- |-----------:|---------:|---------:|----------:|
    | BaseQuery                | 1,928.1 us | 35.81 us | 57.83 us | 252.33 KB |
    | MaterializeOptimization  |   651.2 us |  9.58 us |  8.00 us |   9.43 KB |
    | WhereOptimization        |   647.4 us |  7.49 us |  6.64 us |   8.13 KB |
    | OrderOptimization        |   686.7 us | 12.10 us | 11.32 us |   8.13 KB |
    | AsNoTrackingOptimization |   710.9 us | 12.85 us | 12.02 us |   8.69 KB |
    | AsyncOptimization        |   764.0 us | 14.67 us | 19.08 us |  11.94 KB |
     */
