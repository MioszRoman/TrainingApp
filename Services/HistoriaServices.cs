using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using TreningApp.Models;

namespace TreningApp;

class HistoriaService
{
    private List<HistoriaTreningu> historia = new();
    private readonly string sciezkaHistorii = Path.Combine("Data", "Historia.json");
    private readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        TypeInfoResolver = new DefaultJsonTypeInfoResolver(),
        WriteIndented = true
    };
    public void ZapisHistorii()
    {
        string jsonString = JsonSerializer.Serialize(historia, options);
        File.WriteAllText(sciezkaHistorii, jsonString);
    }
    public void OdczytHistorii()
    {
        if(!File.Exists(sciezkaHistorii))
        {
            return;
        }
        string zapis = File.ReadAllText(sciezkaHistorii);
        if(string.IsNullOrWhiteSpace(zapis))
        {
            historia = new List<HistoriaTreningu>();
            return;
        }
        var des = JsonSerializer.Deserialize<List<HistoriaTreningu>>(zapis, options);
        if(des != null)
        {
            historia = des;
        }
    }

    public List<HistoriaTreningu> PobierzWszystkieWpisy()
    {
        return historia
        .OrderByDescending(h => h.DataTreningu)
        .ToList();
    }
    public List<HistoriaTreningu> PobierzWpisyZDat(DateTime od, DateTime doKiedy)
    {
        return historia
        .Where(h => h.DataTreningu.Date >= od.Date && h.DataTreningu.Date <= doKiedy.Date)
        .OrderByDescending(h => h.DataTreningu)
        .ToList();
    }

    public List<HistoriaTreningu> PobierzWpisyPlanu(int id)
    {
        return historia
        .Where(h => h.IdPlanu == id)
        .OrderByDescending(h => h.DataTreningu)
        .ToList();
    }
    public void ZapiszHistorie(Plan planDoZaczecia, DateTime startTreningu, TimeSpan czasTrwaniaTreningu)
    {
        historia.Add(new HistoriaTreningu(planDoZaczecia.Id, planDoZaczecia.Nazwa, startTreningu, czasTrwaniaTreningu.TotalSeconds));
        ZapisHistorii();
    }
    public int UsunWpisHistoriiPoId(int id)
    {
        if(historia.Count == 0)
        {
            return 0;
        }
        int ileUsunieto = historia.RemoveAll(h => h.IdPlanu == id);
        if(ileUsunieto == 0)
        {
            return -1;
        }
        else
        {
            ZapisHistorii();
            return ileUsunieto;
        }
    }
}