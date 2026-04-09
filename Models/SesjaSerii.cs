using System;

namespace TreningApp;

class SesjaSerii
{
    public Plan PlanTreningowy {get; set;}
    public Cwiczenie Cwiczenie {get; set;}
    public int NumerSerii {get; set;}
    public DateTime DataRozpoczeciaSerii {get; set;}
    public TimeSpan CzasTrwaniaSerii {get; set;}
    public int NumerObwodu {get; set;}
    public SesjaSerii(Plan planTreningowy, Cwiczenie cwiczenie, int numerSerii, DateTime dataRozpoczeciaSerii, TimeSpan czasTrwaniaSerii, int numerObwodu)
    {
        PlanTreningowy = planTreningowy;
        Cwiczenie = cwiczenie;
        NumerSerii = numerSerii;
        DataRozpoczeciaSerii = dataRozpoczeciaSerii;
        CzasTrwaniaSerii = czasTrwaniaSerii;
        NumerObwodu = numerObwodu;
    }
    public void WyswietlSesjeSerii()
    {
        Console.WriteLine("===========================================");
        Console.WriteLine("Obwod: " + NumerObwodu);
        Console.WriteLine("Ćwiczenie: " + Cwiczenie.NazwaCwiczenia);
        Console.WriteLine("Seria: " + NumerSerii + " z " + Cwiczenie.LiczbaSerii);
        Console.WriteLine("Data rozpoczęcia serii: " + DataRozpoczeciaSerii);
        Console.WriteLine("Czas trwania serii: " + CzasTrwaniaSerii.TotalSeconds + " sekund");
        Console.WriteLine("===========================================");
    }
}