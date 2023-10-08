using BenchmarkDotNet.Attributes;
using Conf2023.EFmodel;
using Microsoft.EntityFrameworkCore;

namespace Conf2023.EF;

[MemoryDiagnoser(false)]
public class CompiledQuery
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
    public async Task Query()
    {
        var book = await _appDbContext.Books
            .AsNoTracking()
            .Where(x => x.AuthorId == 20 && x.Year == 5)
            .FirstOrDefaultAsync();
    }

    private static readonly Func<AppDbContext, int, int, Task<Book?>> FindBookByAuthorAndYearAsync =
        Microsoft.EntityFrameworkCore.EF.CompileAsyncQuery(
            (AppDbContext context, int authorId, int year) => 
                context.Books.AsNoTracking().Where(x => x.AuthorId == 20 && x.Year == 5).FirstOrDefault()
            );

    [Benchmark]
    public async Task QueryCompiled()
    {
        var book = await FindBookByAuthorAndYearAsync(_appDbContext, 20, 5);
    }
}

    /* Sqlite
    | Method        | Mean     | Error    | StdDev   | Allocated |
    |-------------- |---------:|---------:|---------:|----------:|
    | Query         | 70.15 us | 0.281 us | 0.234 us |  12.42 KB |
    | QueryCompiled | 30.17 us | 0.131 us | 0.102 us |   5.58 KB |
        */

    /* MsSql
    | Method        | Mean     | Error    | StdDev   | Allocated |
    |-------------- |---------:|---------:|---------:|----------:|
    | Query         | 898.8 us | 17.62 us | 32.65 us |  13.46 KB |
    | QueryCompiled | 853.7 us |  9.55 us |  8.94 us |   6.61 KB |
        */

    /* MsSql, .Net 8.0
    | Method        | Mean     | Error    | StdDev   | Allocated |
    |-------------- |---------:|---------:|---------:|----------:|
    | Query         | 961.5 us | 19.06 us | 40.62 us |  11.33 KB |
    | QueryCompiled | 904.5 us | 17.71 us | 34.95 us |   6.51 KB |
    */
