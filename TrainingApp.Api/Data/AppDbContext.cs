
using Microsoft.EntityFrameworkCore;
using TrainingApp.Api.Models;

namespace TrainingApp.Api.Data;
public class AppDbContext : DbContext
{
    public DbSet<Plan> Plany {get; set;}
    public DbSet<Cwiczenie> Cwiczenia {get; set;}
    public DbSet<HistoriaTreningu> HistoriaTreningow {get; set;}
    public DbSet<User> Users {get; set;}
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}