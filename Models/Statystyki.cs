namespace TreningApp.Models;

public class Statystyki
{
    public int IloscTreningow {get; set;}
    public int LacznyCzas {get; set;}
    public int SredniCzas {get; set;}
    public WynikTreningu NajdluzszyTrening {get; set;}
    public WynikTreningu NajkrotszyTrening {get; set;}
    public WynikTreningu NajdluzszyTreningWTygodniu {get; set;}
    public Wykonanie Najczestszy {get; set;}
    public List<Wykonanie> PoszczegolneWykonania {get; set;}
    public Statystyki(
        int iloscTreningow, int lacznyCzas, int sredniCzas, 
        WynikTreningu najdluzszyTrening, WynikTreningu najkrotszyTrening,
        WynikTreningu najdluzszyTreningWTygodniu, Wykonanie najczestszy, 
        List<Wykonanie> poszczegolneWykonania
    )
    {
        IloscTreningow = iloscTreningow;
        LacznyCzas = lacznyCzas;
        SredniCzas = sredniCzas;
        NajdluzszyTrening = najdluzszyTrening;
        NajkrotszyTrening = najkrotszyTrening;
        NajdluzszyTreningWTygodniu = najdluzszyTreningWTygodniu;
        Najczestszy = najczestszy;
        PoszczegolneWykonania = poszczegolneWykonania;
    }
}