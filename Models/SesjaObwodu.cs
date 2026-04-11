using System;

namespace TreningApp.Models;

public class SesjaObwodu
{
    public Plan PlanTreningowy {get; set;}
    public DateTime DataRozpoczecia {get; set;}
    public TimeSpan CzasTrwaniaObwodu {get; set;}
    public int NumerObwodu {get; set;}
    public SesjaObwodu(Plan planTreningowy, DateTime dataRozpoczecia, TimeSpan czasTrwaniaObwodu, int numerObwodu)
    {
        PlanTreningowy = planTreningowy;
        DataRozpoczecia = dataRozpoczecia;
        CzasTrwaniaObwodu = czasTrwaniaObwodu;
        NumerObwodu = numerObwodu;
    }
}