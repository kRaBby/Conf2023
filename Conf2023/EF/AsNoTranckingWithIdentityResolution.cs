using BenchmarkDotNet.Attributes;
using Conf2023.EFmodel;
using Microsoft.EntityFrameworkCore;

namespace Conf2023.EF;

[MemoryDiagnoser(false)]
public class AsNoTranckingWithIdentityResolution
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
    public async Task AsNoTracking()
    {
        var books = await _appDbContext.Books.Include(x => x.Author).AsNoTracking().Select(x => new { x.Title, x.Author }).ToArrayAsync();

        var sum = 0;
        foreach (var item in books)
        {
            sum += item.Author.Id;
        }
    }

    [Benchmark]
    public async Task AsNoTrackingWithIdentityResolution()
    {
        var books = await _appDbContext.Books.Include(x => x.Author).AsNoTrackingWithIdentityResolution().Select(x => new { x.Title, x.Author }).ToArrayAsync();

        var sum = 0;
        foreach (var item in books)
        {
            sum += item.Author.Id;
        }
    }
}
/* Sqlite
| Method                             | Mean     | Error     | StdDev    | Allocated  |
|----------------------------------- |---------:|----------:|----------:|-----------:|
| AsNoTracking                       | 1.028 ms | 0.0205 ms | 0.0202 ms |  455.74 KB |
| AsNoTrackingWithIdentityResolution | 3.126 ms | 0.0985 ms | 0.2858 ms | 1095.42 KB |
 */

/* MsSql
| Method                             | Mean     | Error     | StdDev    | Allocated |
|----------------------------------- |---------:|----------:|----------:|----------:|
| AsNoTracking                       | 2.524 ms | 0.0478 ms | 0.0490 ms | 392.24 KB |
| AsNoTrackingWithIdentityResolution | 2.591 ms | 0.0456 ms | 0.0427 ms | 445.53 KB |
 */

/* MsSql, .Net 8.0
| Method                             | Mean     | Error     | StdDev    | Allocated |
|----------------------------------- |---------:|----------:|----------:|----------:|
| AsNoTracking                       | 2.604 ms | 0.0519 ms | 0.1254 ms | 389.93 KB |
| AsNoTrackingWithIdentityResolution | 2.672 ms | 0.0505 ms | 0.0801 ms | 434.25 KB |
 */