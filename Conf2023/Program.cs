using BenchmarkDotNet.Running;
using Conf2023.EF;
using Conf2023.EFmodel;
using Conf2023.NET;

//BenchmarkRunner.Run<StringBenchmark>();
//BenchmarkRunner.Run<ForVsForeachBenchmark>();
//BenchmarkRunner.Run<ArraySkipTake>();
//BenchmarkRunner.Run<AnyVsExistsBenchmark>();
//BenchmarkRunner.Run<StringCompareBenchmark>();
//BenchmarkRunner.Run<AddRangeBenchmark>();


//DBSetup();

//BenchmarkRunner.Run<RepositoryVsIQueryable>();
//BenchmarkRunner.Run<ToListVsToArray>();
//BenchmarkRunner.Run<AsNoTranckingWithIdentityResolution>();
//BenchmarkRunner.Run<CompiledQuery>();
//BenchmarkRunner.Run<QueryBenchmark>();


return;



void DBSetup()
{
    using (var context = new AppDbContext())
    {
        context.Books.RemoveRange(context.Books);
        context.Authors.RemoveRange(context.Authors);

        context.SaveChanges();

        for (int i = 0; i < 50; i++)
        {
            var author = new Author { Name = $"Author {i}" };
            context.Authors.Add(author);

            for (int j = 0; j < 10; j++)
            {
                var book = new Book { Title = $"Book {j} of author {i}", Author = author, Year = j };
                context.Books.Add(book);
            }
        }

        context.SaveChanges();
    }
}