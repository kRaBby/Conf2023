using BenchmarkDotNet.Attributes;
using Conf2023.EFmodel;
using Microsoft.EntityFrameworkCore;

namespace Conf2023.EF;

[MemoryDiagnoser(false)]
public class RepositoryVsIQueryable
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
    public async Task Repository()
    {
        var books = await _appDbContext.Books.AsNoTracking().ToArrayAsync();

        var sum = 0;
        foreach (var book in books) 
        {
            sum += book.Year;
        }
    }


    [Benchmark]
    public async Task IQueryable()
    {
        var booksQuery = _appDbContext.Books.AsNoTracking();

        var years = await booksQuery.Select(x => x.Year).ToArrayAsync();

        var sum = 0;
        foreach (var year in years)
        {
            sum += year;
        }
    }
}

    /* Sqlite, .Net 6.0
    | Method     | Mean     | Error   | StdDev  | Allocated |
    |----------- |---------:|--------:|--------:|----------:|
    | Repository | 336.2 us | 1.94 us | 1.82 us | 168.29 KB |
    | IQueryable | 168.5 us | 2.45 us | 2.29 us | 110.29 KB |
     */

    /* MsSql, .Net 6.0
    | Method     | Mean     | Error     | StdDev    | Allocated |
    |----------- |---------:|----------:|----------:|----------:|
    | Repository | 1.787 ms | 0.0355 ms | 0.0780 ms | 301.33 KB |
    | IQueryable | 1.075 ms | 0.0188 ms | 0.0157 ms | 110.17 KB |
     */

    /* MsSql, .Net 8.0
    | Method     | Mean     | Error     | StdDev    | Allocated |
    |----------- |---------:|----------:|----------:|----------:|
    | Repository | 1.672 ms | 0.0332 ms | 0.0736 ms | 300.45 KB |
    | IQueryable | 1.060 ms | 0.0207 ms | 0.0328 ms | 108.26 KB |
     */