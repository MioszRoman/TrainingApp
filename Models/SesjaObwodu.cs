using System;

namespace TreningApp;

class SesjaObwodu
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
    public void WyswietlSesjeObwodu()
    {
        Console.WriteLine("===========================================");
        Console.WriteLine("Obwod: " + NumerObwodu);
        Console.WriteLine("Data rozpoczęcia obwodu: " + DataRozpoczecia);
        Console.WriteLine("Czas trwania obwodu: " + CzasTrwaniaObwodu.TotalSeconds + " sekund");
        Console.WriteLine("===========================================");
    }
}