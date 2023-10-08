using BenchmarkDotNet.Attributes;
using Conf2023.EFmodel;
using Microsoft.EntityFrameworkCore;

namespace Conf2023.EF;
[MemoryDiagnoser(false)]
public class ToListVsToArray
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
    public async Task ToArrayAsync()
    {
        var books = await _appDbContext.Books.AsNoTracking().ToArrayAsync();
    }


    [Benchmark]
    public async Task ToListAsync()
    {
        var books = await _appDbContext.Books.AsNoTracking().ToListAsync();
    }
}
/* Sqlite
| Method       | Mean     | Error   | StdDev   | Allocated |
|------------- |---------:|--------:|---------:|----------:|
| ToArrayAsync | 424.5 us | 8.30 us | 13.17 us | 168.12 KB |
| ToListAsync  | 394.9 us | 7.58 us |  7.09 us | 164.12 KB |
 */

/* MsSql
| Method       | Mean     | Error     | StdDev    | Allocated |
|------------- |---------:|----------:|----------:|----------:|
| ToArrayAsync | 2.287 ms | 0.0423 ms | 0.0504 ms | 301.33 KB |
| ToListAsync  | 2.010 ms | 0.0395 ms | 0.0422 ms | 297.27 KB |
 */


/* MsSql, .Net 8.0
| Method       | Mean     | Error     | StdDev    | Allocated |
|------------- |---------:|----------:|----------:|----------:|
| ToArrayAsync | 1.720 ms | 0.0340 ms | 0.0559 ms | 300.45 KB |
| ToListAsync  | 1.729 ms | 0.0344 ms | 0.0584 ms |  296.4 KB | 
 */