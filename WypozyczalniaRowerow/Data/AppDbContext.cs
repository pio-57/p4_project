using Microsoft.EntityFrameworkCore;
using System.Configuration;

public class AppDbContext : DbContext
{
    public DbSet<Klient> Klienci { get; set; }
    public DbSet<Rower> Rowery { get; set; }
    public DbSet<Wypozyczenie> Wypozyczenia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var conn = ConfigurationManager
            .ConnectionStrings["DefaultConnection"]
            .ConnectionString;

        options.UseSqlServer(conn);
    }


}

