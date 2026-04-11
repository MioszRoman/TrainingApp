using System;

namespace TreningApp.Models;

public class SesjaSerii
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
}