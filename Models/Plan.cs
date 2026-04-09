using System;
using System.Collections.Generic;

namespace TreningApp;

class Plan
{
    public string Nazwa {get; set;}
    public int Poziom {get; set;}
    public string Rodzaj {get; set;}
    public int IloscObwodow {get; set;}
    public int Id {get; set;}
    public int PrzerwaMiedzyObwodami {get; set;}
    public List<Cwiczenie> Cwiczenia {get; set;}
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
    public void WyswietlPlan()
    {
        Console.WriteLine("Id treningu: " + Id);
        Console.WriteLine("Trening: " + Nazwa);
        Console.WriteLine("Poziom trudności: " + Poziom);
        Console.WriteLine("Rodzaj: " + Rodzaj);
        Console.WriteLine("Ilość obwodów: " + IloscObwodow);
        Console.WriteLine("Ilość ćwiczeń: " + Cwiczenia.Count);
        Console.WriteLine("-------------------------------------------");
        //Cwiczenia.ForEach(c => c.WyswietlCwiczenie()); - Druga opcja wyswietlania cwiczen, trochę bardziej elegancka i na wyższy poziom, 
        //ale nieco mniej czytelna dla początkujących programistów, dlatego zostawiłem tą bardziej klasyczną pętlę foreach.
    }
    public void WyswietlCwiczenia()
        {
            Console.WriteLine("Ćwiczenia: ");
            foreach(var cwiczenie in Cwiczenia)
            {
                cwiczenie.WyswietlCwiczenie();
            }
        }
}