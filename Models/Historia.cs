using System;

namespace TreningApp.Models;

public class HistoriaTreningu
{
    public int IdPlanu {get; set;}
    public string NazwaPlanu {get; set;}
    public DateTime DataTreningu {get; set;}
    public double CzasTrwania {get; set;}
    public HistoriaTreningu(int idPlanu, string nazwaPlanu, DateTime dataTreningu, double czasTrwania)
    {
        IdPlanu = idPlanu;
        NazwaPlanu = nazwaPlanu;
        DataTreningu = dataTreningu;
        CzasTrwania = czasTrwania;
    }
}