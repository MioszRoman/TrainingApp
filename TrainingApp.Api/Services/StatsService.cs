using TrainingApp.Api.Data;
using TrainingApp.Api.Dtos;
using System.Linq;

namespace TrainingApp.Api.Services;
public class StatsService : IStatsService
{
    private readonly AppDbContext _context;
    public StatsService(AppDbContext context)
    {
        _context = context;
    }
    public StatsDto? GetStats()
    {
        var historia = _context.HistoriaTreningow.ToList();
        if(historia.Count == 0)
        {
            return null;
        }
        return new StatsDto
        {
            LiczbaTreningow = historia.Count,
            LacznyCzasSekundy = historia.Sum(h => h.CzasTrwania),
            SredniCzasSekundy = historia.Average(h => h.CzasTrwania),
            NajdluzszyTreningSekundy = historia.Max(h => h.CzasTrwania),
            NajkrotszyTreningSekundy = historia.Min(h => h.CzasTrwania)
        };

    }
}