using TrainingApp.Api.Data;
using TrainingApp.Api.Dtos;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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
    public MostFrequentPlanDto? GetMostFrequentPlan()
    {
        var historia = _context.HistoriaTreningow.ToList();
        if(historia.Count == 0) return null;
        var result = historia
        .GroupBy(h => h.NazwaPlanu)
        .Select(g => new
        {
            Nazwa = g.Key,
            Ilosc = g.Count()
        })
        .OrderByDescending(x => x.Ilosc)
        .FirstOrDefault();
        if(result == null) return null;
        return new MostFrequentPlanDto
        {
            NazwaPlanu = result.Nazwa,
            IloscWykonan = result.Ilosc
        };
    }
}