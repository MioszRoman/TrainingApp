using System;

namespace TreningApp;

class SesjaTreningowa
{
    public Plan PlanTreningowy {get; set;}
    public DateTime DataTreningu {get; set;}
    public TimeSpan CzasTrwaniaTreningu {get; set;}
    public SesjaTreningowa(Plan planTreningowy, DateTime dataTreningu, TimeSpan czasTrwaniaTreningu)
    {
        PlanTreningowy = planTreningowy;
        DataTreningu = dataTreningu;
        CzasTrwaniaTreningu = czasTrwaniaTreningu;
    }
    public void WyswietlSesje()
    {
        Console.WriteLine("===========================================");
        Console.WriteLine("Plan treningowy: " + PlanTreningowy.Nazwa);
        Console.WriteLine("Data treningu: " + DataTreningu);
        Console.WriteLine("Czas trwania treningu: " + CzasTrwaniaTreningu.TotalSeconds + " sekund");
        Console.WriteLine("===========================================");
    }
}