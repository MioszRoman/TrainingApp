using System;

namespace TreningApp.Models;

public class SesjaTreningowa
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
}