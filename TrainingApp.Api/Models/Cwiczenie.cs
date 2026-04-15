using System;

namespace TrainingApp.Api.Models;

public class Cwiczenie
{
    public int Id {get; set;}
    public int PlanId {get; set;}
    public string NazwaCwiczenia {get; set;} = "";
    public int LiczbaSerii {get; set;}
    public int LiczbaPowtorzen {get; set;}
    public int PrzerwaMiedzySeriami {get; set;}
    public Plan? Plan {get; set;}
    public Cwiczenie()
    {
        
    }
    public Cwiczenie(string nazwaCwiczenia, int liczbaSerii, int liczbaPowtorzen, int przerwaMiedzySeriami)
    {
        NazwaCwiczenia = nazwaCwiczenia;
        LiczbaSerii = liczbaSerii;
        LiczbaPowtorzen = liczbaPowtorzen;
        PrzerwaMiedzySeriami = przerwaMiedzySeriami;
    }
}