
using Microsoft.EntityFrameworkCore;
using TreningApp.Models;

public class AppDbContext : DbContext
{
    public DbSet<Plan> Plany {get; set;}
    public DbSet<Cwiczenie> Cwiczenia {get; set;}
    public DbSet<HistoriaTreningu> HistoriaTreningow {get; set;}
    private string ConnectionString = "Data Source=Data/DataBase.db";
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(ConnectionString);

    }
}