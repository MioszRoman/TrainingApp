using System;
using System.Collections.Generic;

namespace TreningApp.Models;

public class Plan
{
    public string Nazwa {get; set;} = "";
    public int Poziom {get; set;}
    public string Rodzaj {get; set;} = "";
    public int IloscObwodow {get; set;}
    public int Id {get; set;}
    public int PrzerwaMiedzyObwodami {get; set;}
    public List<Cwiczenie> Cwiczenia {get; set;} = new();
    public List<HistoriaTreningu> HistoriaTreningow {get; set;} = new();
    public Plan()
    {
        
    }
    public Plan(string nazwa, int poziom, string rodzaj, int iloscObwodow,int id, int przerwaMiedzyObwodami, List<Cwiczenie> cwiczenia)
    {
        Nazwa = nazwa;
        Poziom = poziom;
        Rodzaj = rodzaj;
        IloscObwodow = iloscObwodow;
        PrzerwaMiedzyObwodami = przerwaMiedzyObwodami;
        Id = id;
        Cwiczenia = cwiczenia;
    }
}