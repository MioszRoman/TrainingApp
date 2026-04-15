using System;

namespace TrainingApp.Api.Models;

public class HistoriaTreningu
{
    public int Id {get; set;}
    public int PlanId {get; set;}
    public string NazwaPlanu {get; set;} = "";
    public DateTime DataTreningu {get; set;}
    public double CzasTrwania {get; set;}
    public Plan? Plan {get; set;}
    public HistoriaTreningu()
    {
        
    }
    public HistoriaTreningu(int idPlanu, string nazwaPlanu, DateTime dataTreningu, double czasTrwania)
    {
        PlanId = idPlanu;
        NazwaPlanu = nazwaPlanu;
        DataTreningu = dataTreningu;
        CzasTrwania = czasTrwania;
    }
}