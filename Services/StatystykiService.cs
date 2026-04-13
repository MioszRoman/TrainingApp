using System;
using System.Linq;
using TreningApp.Models;
using TreningApp.UI;
class StatystykiService
{
    private readonly ConsoleRenderer renderer = new ConsoleRenderer(); 

    public string FormatujCzas(double calyCzas)
    {
        int wszystkieSekundy = (int)calyCzas;
        int minuty = wszystkieSekundy / 60;
        int sekundy = wszystkieSekundy % 60;
        string komunikat = $"{minuty} min {sekundy} s";
        return komunikat;
    }

    public void WyswietlStatystyki(List<HistoriaTreningu> historia)
    {
        //Ilość treningów 
        if(historia.Count == 0)
        {
            renderer.PokazBlad("Brak historii treningów.");
            return;
        }
        renderer.PokazKomunikat($"Ilość treningów: {historia.Count}");
        renderer.PokazKomunikat($"Łączny czas treningów: {LacznyCzas(historia)}");
        renderer.PokazKomunikat($"Średni czas treningów: {SredniCzas(historia)}");
        NajczesciejWykonywanyTrening(historia);
        PoszczegolneWykonania(historia);
        NajdluzszyTrening(historia);
        NajkrotszyTrening(historia);
        NajdluzszyWTygodniu(historia);        
    }
    public void NajdluzszyTrening(List<HistoriaTreningu> historia)
    {
        var najdluzszy = historia.MaxBy(x => x.CzasTrwania);
        renderer.PokazKomunikat($"Najdłuższy trening: {najdluzszy.NazwaPlanu} trwał {FormatujCzas(najdluzszy.CzasTrwania)}");
    }
    public void NajkrotszyTrening(List<HistoriaTreningu> historia)
    {
        var najkrotszy = historia.MinBy(x => x.CzasTrwania);
        renderer.PokazKomunikat($"Najkrotszy trening to: {najkrotszy.NazwaPlanu} trwał {FormatujCzas(najkrotszy.CzasTrwania)}");
    }
    public double WszystkieSekundy(List<HistoriaTreningu> historia)
    {
        return historia.Sum(x => x.CzasTrwania);
    }
    public string SredniCzas(List<HistoriaTreningu> historia)
    {
        double czas = WszystkieSekundy(historia);
        string sredniCzas = FormatujCzas((czas / historia.Count));
        return sredniCzas;
        //Console.WriteLine($"Średni czas treningów {sredniCzas}");
    
    }
    public string LacznyCzas(List<HistoriaTreningu> historia)
    {
        double czas = WszystkieSekundy(historia);
        string ladnyCzas = FormatujCzas(czas);
        return ladnyCzas;
        //Console.WriteLine($"Łączny czas treningów {ladnyCzas}");

    }
    public void NajczesciejWykonywanyTrening(List<HistoriaTreningu> historia)
    {
        var najczestszy = historia
        .GroupBy(x => x.NazwaPlanu) //Grupujemy identyczne elementy
        .OrderByDescending(g => g.Count()) //Sortujemy po liczbie wystąpień
        .Select(g => g.Key) //Wybieramy nazwę elementu (klucz)
        .FirstOrDefault(); //Wynik
        renderer.PokazKomunikat($"Najczęściej występujący trening to: {najczestszy}");

    }
    public void PoszczegolneWykonania(List<HistoriaTreningu> historia)
    {
        var grupy = historia
        .GroupBy(x => x.NazwaPlanu)
        .OrderByDescending(g => g.Count());
        renderer.PokazKomunikat("Poszczególne wystąpienia treningów: ");
        foreach(var grupa in grupy)
        {
            renderer.PokazKomunikat($"{grupa.Key} -> {grupa.Count()}");
        }
    }

    public void NajdluzszyWTygodniu(List<HistoriaTreningu> historia)
    {
        DateTime dzisiaj = DateTime.Now;
        DateTime ostatniTydzien = dzisiaj.AddDays(-7);
        var historiaZTygodnia = historia.Where(x => x.DataTreningu >= ostatniTydzien);
        if(!historiaZTygodnia.Any())
        {
            renderer.PokazBlad("Nie ma treningów w ostatnim tygodniu.");
            return;
        }
        var najdluzszyWTygodniu = historiaZTygodnia.MaxBy(x => x.CzasTrwania);
        renderer.PokazKomunikat($"Najdłuższy trening w ostatnim tygodniu to: {najdluzszyWTygodniu.NazwaPlanu} trwał on: {FormatujCzas(najdluzszyWTygodniu.CzasTrwania)}");
    }
}