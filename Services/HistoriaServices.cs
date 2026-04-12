using System;
using System.Threading;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using TreningApp.Models;
using TreningApp.UI;

namespace TreningApp;

class HistoriaService
{
    private List<HistoriaTreningu> historia = new List<HistoriaTreningu>();
    private string sciezkaHistorii = Path.Combine("Data", "Historia.json");
    private JsonSerializerOptions JsonOptions()
    {
        var options = new JsonSerializerOptions();
        options.TypeInfoResolver = new DefaultJsonTypeInfoResolver();
        options.WriteIndented = true;
        return options;
    }
    public void ZapisHistorii()
    {
        string jsonString = JsonSerializer.Serialize(historia, JsonOptions());
        File.WriteAllText(sciezkaHistorii, jsonString);
        //Console.WriteLine("Zapisano pomyślnie do pliku Plany.json");
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
        var des = JsonSerializer.Deserialize<List<HistoriaTreningu>>(zapis, JsonOptions());
        if(des != null)
        {
            historia = des;
        }
    }
    public void UsunWpisHistoriiPoId(int id)
    {
        if(historia.Count == 0)
        {
            Console.WriteLine("Brak historii treningów.");
            return;
        }
        int ileUsunieto = historia.RemoveAll(h => h.IdPlanu == id);
        if(ileUsunieto == 0)
        {
            Console.WriteLine("Nie usunięto żadnego planu.");
        }
        else
        {
            ZapisHistorii();
            Console.WriteLine($"Pomyślnie usunięto {ileUsunieto} wpisów.");
        }
    }
    public void WyswietlHistorieTreningu()
    {
        if(historia.Count == 0)
            {
                Console.WriteLine("Brak historii treningów.");
                return;
            }else
            {
            Console.WriteLine("Historia Treningów:");
            }
        var posortowane = historia.OrderByDescending(h => h.DataTreningu).ToList();
        foreach(var historiaPlanu in posortowane)
        {
                WyswietlHistorie(historiaPlanu);
        }
    }
    public void WyswietlHistorie(HistoriaTreningu historiaPlanu)
    {
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine($"ID treningu:  {historiaPlanu.IdPlanu}");
        Console.WriteLine("Nazwa planu: " + historiaPlanu.NazwaPlanu);
        Console.WriteLine("Data treningu: " + historiaPlanu.DataTreningu.ToString("dd.MM.yyyy HH:mm"));
        int wszystkieSekundy = (int)historiaPlanu.CzasTrwania;
        int minuty = wszystkieSekundy / 60;
        int sekundy = wszystkieSekundy % 60;
        Console.WriteLine("Czas trwania: " + minuty + " min " + sekundy.ToString("00") + "s");
        Console.WriteLine("-------------------------------------------");
    }
    public void WyswietlHistorieZData(DateTime od, DateTime doKiedy)
    {
        if(historia.Count == 0)
        {
            Console.WriteLine("Brak historii treningów!");
            return;
        }
        Console.WriteLine($"Historia od {od:dd.MM.yyyy} do {doKiedy:dd.MM.yyyy}.");
        bool czyZnalezionoPlan = false;
        foreach(var historiaPlanu in historia)
        {
            if(historiaPlanu.DataTreningu.Date >= od.Date && historiaPlanu.DataTreningu.Date <= doKiedy.Date)
            {
                WyswietlHistorie(historiaPlanu);
                czyZnalezionoPlan = true;
            }
        }
        if(czyZnalezionoPlan == false)
        {
            Console.WriteLine("Nie znaleziono historii w podanym zakresie!");
            return;
        }
    }
    public void WyswietlHistoriePlanu(int id)
    {
        if(historia.Count == 0)
        {
            Console.WriteLine("Brak historii treningów!");
            return;
        }
        bool czyZnaleziono = false;
        foreach(var historiaPlanu in historia)
        {
            if(historiaPlanu.IdPlanu == id)
            {
                
                WyswietlHistorie(historiaPlanu);
                czyZnaleziono = true;
            }
        }
        if(czyZnaleziono == false)
            {
            Console.WriteLine("Nie znaleziono takiego planu!");
            return;
            }
    }
}