using System;
using TreningApp.Models;

namespace TreningApp;

class HistoriaService
{
    private AppDbContext _context;
    public HistoriaService(AppDbContext context)
    {
        _context = context;
    }
    public void DodajWpisHistorii(int planId, string nazwaPlanu, DateTime dataTreningu, double czasTrwania)
    {
        HistoriaTreningu wpisHistorii = new HistoriaTreningu(planId, nazwaPlanu, dataTreningu, czasTrwania);
        _context.HistoriaTreningow.Add(wpisHistorii);
        _context.SaveChanges();
    }
    public List<HistoriaTreningu> GetHistoria()
    {
        return _context.HistoriaTreningow.ToList();
    }
    public List<HistoriaTreningu> PobierzWszystkieWpisy()
    {
        return _context.HistoriaTreningow
        .OrderByDescending(h => h.DataTreningu)
        .ToList();
    }
    public List<HistoriaTreningu> PobierzWpisyZDat(DateTime od, DateTime doKiedy)
    {
        return _context.HistoriaTreningow
        .Where(h => h.DataTreningu.Date >= od.Date && h.DataTreningu.Date <= doKiedy.Date)
        .OrderByDescending(h => h.DataTreningu)
        .ToList();
    }

    public List<HistoriaTreningu> PobierzWpisyPlanu(int id)
    {
        return _context.HistoriaTreningow
        .Where(h => h.PlanId == id)
        .OrderByDescending(h => h.DataTreningu)
        .ToList();
    }
    public void ZapiszHistorie(Plan planDoZaczecia, DateTime startTreningu, TimeSpan czasTrwaniaTreningu)
    {
        DodajWpisHistorii(planDoZaczecia.Id, planDoZaczecia.Nazwa, startTreningu, czasTrwaniaTreningu.TotalSeconds);
    }
    public int UsunWpisHistoriiPoId(int id)
    {
        List<HistoriaTreningu> wpisyDoUsuniecia = _context.HistoriaTreningow
        .Where(historia => historia.PlanId == id)
        .ToList();
        if(wpisyDoUsuniecia.Count == 0)
        {
            return -1;
        }
        _context.HistoriaTreningow.RemoveRange(wpisyDoUsuniecia);
        _context.SaveChanges();
        return wpisyDoUsuniecia.Count;
    }
}