using System;

namespace TreningApp.Models;

public class WynikTreningu
{
    public string Nazwa {get; set;}
    public int Czas {get; set;}
    public DateTime Data {get; set;}
    public WynikTreningu(string nazwa, int czas, DateTime data)
    {
        Nazwa = nazwa;
        Czas = czas;
        Data = data;
    }
}