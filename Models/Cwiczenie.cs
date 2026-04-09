using System;

namespace TreningApp;

class Cwiczenie
{
    public string NazwaCwiczenia {get; set;}
    public int LiczbaSerii {get; set;}
    public int LiczbaPowtorzen {get; set;}
    public int PrzerwaMiedzySeriami {get; set;}
    public Cwiczenie(string nazwaCwiczenia, int liczbaSerii, int liczbaPowtorzen, int przerwaMiedzySeriami)
    {
        NazwaCwiczenia = nazwaCwiczenia;
        LiczbaSerii = liczbaSerii;
        LiczbaPowtorzen = liczbaPowtorzen;
        PrzerwaMiedzySeriami = przerwaMiedzySeriami;
    }
    public void WyswietlCwiczenie()
    {
        Console.WriteLine("Ćwiczenie: " + NazwaCwiczenia);
        Console.WriteLine("Serii: " + LiczbaSerii);
        Console.WriteLine("Powtorzeń: " + LiczbaPowtorzen);
        Console.WriteLine("Przerwa miedzy seriami: " + PrzerwaMiedzySeriami + " sekund");
        Console.WriteLine("===========================================");
    }
}