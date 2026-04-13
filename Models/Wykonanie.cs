namespace TreningApp.Models;

public class Wykonanie
{
    public string Nazwa {get; set;}
    public int Ilosc {get; set;}
    public Wykonanie(string nazwa, int ilosc)
    {
        Nazwa = nazwa;
        Ilosc = ilosc;
    }
}