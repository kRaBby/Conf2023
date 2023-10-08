using Microsoft.EntityFrameworkCore;

namespace Conf2023.EFmodel;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlite("Data Source=C:\\Work\\E\\Conf2023\\Conf2023\\books.db");
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=books;User Id=sa;Password=Q1w2e3r4t5y6;TrustServerCertificate=true;");
    }
}
