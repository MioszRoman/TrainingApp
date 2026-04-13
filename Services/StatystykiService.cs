using System;
using System.Linq;
using TreningApp.Models;
using TreningApp.UI;
class StatystykiService
{
    public Statystyki PobierzStatystyki(List<HistoriaTreningu> historia)
    {
        if(historia.Count() == 0)
        {
            return null;
        }
        int ilosc = historia.Count();
        int lacznyCzas = LacznyCzas(historia);
        int sredniCzas = SredniCzas(historia);
        WynikTreningu najdluzszy = NajdluzszyTrening(historia);
        WynikTreningu najkrotszy = NajkrotszyTrening(historia);
        WynikTreningu najdluzszyWTygodniu = NajdluzszyWTygodniu(historia);
        Wykonanie najczestszy = NajczesciejWykonywanyTrening(historia);
        List<Wykonanie> poszczegolne = PoszczegolneWykonania(historia);
        return new Statystyki(ilosc, lacznyCzas, sredniCzas, najdluzszy, najkrotszy, najdluzszyWTygodniu, najczestszy, poszczegolne);
    }
    public WynikTreningu NajdluzszyTrening(List<HistoriaTreningu> historia)
    {
        var najdluzszy = historia.MaxBy(x => x.CzasTrwania);
        if(najdluzszy != null)
        {
            return new WynikTreningu(najdluzszy.NazwaPlanu, (int)najdluzszy.CzasTrwania, najdluzszy.DataTreningu);
        }
        return null;
    }
    public WynikTreningu NajkrotszyTrening(List<HistoriaTreningu> historia)
    {
        var najkrotszy = historia.MinBy(x => x.CzasTrwania);
        if(najkrotszy != null)
        {
        return new WynikTreningu(najkrotszy.NazwaPlanu, (int)najkrotszy.CzasTrwania, najkrotszy.DataTreningu);
        }
        return null;
    }
    public double WszystkieSekundy(List<HistoriaTreningu> historia)
    {
        return historia.Sum(x => x.CzasTrwania);
    }
    public int SredniCzas(List<HistoriaTreningu> historia)
    {
        double czas = WszystkieSekundy(historia);
        int sredniCzas = (int)(czas / historia.Count);
        return sredniCzas;
    }
    public int LacznyCzas(List<HistoriaTreningu> historia)
    {
        double czas = WszystkieSekundy(historia);
        int ladnyCzas = (int)czas;
        return ladnyCzas;
    }
    public Wykonanie NajczesciejWykonywanyTrening(List<HistoriaTreningu> historia)
    {
        var wynik = historia
        .GroupBy(x => x.NazwaPlanu) //Grupujemy identyczne elementy
        .OrderByDescending(g => g.Count()) //Sortujemy po liczbie wystąpień
        .FirstOrDefault(); //Wynik
        if(wynik != null)
        {
            return new Wykonanie(wynik.Key, wynik.Count());
        }
        return null;
    }
    public List<Wykonanie> PoszczegolneWykonania(List<HistoriaTreningu> historia)
    {
        List<Wykonanie> wykonanie = new List<Wykonanie>();
        var grupy = historia
        .GroupBy(x => x.NazwaPlanu)
        .OrderByDescending(g => g.Count());
        foreach(var grupa in grupy)
        {
            Wykonanie pojedyncze = new Wykonanie(grupa.Key, grupa.Count());
            wykonanie.Add(pojedyncze);
        }
        return wykonanie;
    }
    public WynikTreningu NajdluzszyWTygodniu(List<HistoriaTreningu> historia)
    {
        DateTime dzisiaj = DateTime.Now;
        DateTime ostatniTydzien = dzisiaj.AddDays(-7);
        var historiaZTygodnia = historia.Where(x => x.DataTreningu >= ostatniTydzien);
        if(!historiaZTygodnia.Any())
        {
            return null;
        }
        var najdluzszyWTygodniu = historiaZTygodnia.MaxBy(x => x.CzasTrwania);
        return new WynikTreningu(najdluzszyWTygodniu.NazwaPlanu, (int)najdluzszyWTygodniu.CzasTrwania, najdluzszyWTygodniu.DataTreningu);
    }
}