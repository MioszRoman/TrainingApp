using System;
using System.Collections.Generic;
using System.Reflection;
using TreningApp.Models;

namespace TreningApp;

class InputHelper
{
    public bool Decyzja(string komunikat, string blad)
    {
        while(true)
        {
            Console.Write(komunikat);
            string? userInput = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine(blad);
                continue;
            }
            string input = userInput.Trim().ToLower();
            if(input == "tak" || input == "t")
            {
                return true;
            }
            if(input == "nie" || input == "n")
            {
                return false;
            }
            else
            {
                Console.WriteLine(blad);
                continue;
            }
            
        }
    }
    public int PobierzLiczbe(string komunikat, int liczbaMin, int liczbaMax, string blad)
    {   
        while(true)
        {
            Console.Write(komunikat);
            bool czyLiczba = int.TryParse(Console.ReadLine(), out int liczba);
            if(!czyLiczba)
            {
                Console.WriteLine(blad);
                continue;
            }
            if(liczba < liczbaMin)
            {
                Console.WriteLine(blad);
                continue;
            }
            if(liczba > liczbaMax)
            {
                Console.WriteLine(blad);
                continue;
            }
            return liczba;
        }
    }
    public string PobierzTekst(string komunikat, string blad)
    {
        while(true)
        {
            Console.Write(komunikat);
            string? text = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine(blad);
                continue;
            }
            return text;
        }
    }
    public string PobierzTekstDoEdycji(string komunikat)
    {
        Console.Write(komunikat);
        string? text = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(text))
        {
            return "";
        }
        return text;
    }
    public int? PobierzLiczbeDoEdycji(string komunikat, int liczbaMin, int liczbaMax, string blad)
    {
        do
        {
            Console.Write(komunikat);
            string? text = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(text))
            {
                return null;
            }
            bool czyLiczba = int.TryParse(text, out int liczba);
            if(!czyLiczba)
            {
                Console.WriteLine(blad);
                continue;
            }
            if(liczba < liczbaMin)
            {
                Console.WriteLine(blad);
                continue;
            }
            if(liczba > liczbaMax)
            {
                Console.WriteLine(blad);
                continue;
            }
            return liczba;
        }while(true);
    }
    public int PobierzIloscObwodow(int liczbaMin)
    {
        return PobierzLiczbe("Podaj ilość obwodów: ", liczbaMin, 20, "Upewnij się, że podałeś liczbę większą od zera!");
    }
    public int PobierzPrzerweMiedzyObwodami(int iloscObwodow)
    {
        int przerwaO = 0;
        if(iloscObwodow >= 2){
            return PobierzLiczbe("Podaj przerwę między obwodami (w sekundach): ", 0, 800, "Przerwa powinna być liczbą! Od 0 w górę!");
        }
        return przerwaO;
    }
    public int PobierzPoziom()
    {
        return PobierzLiczbe("Podaj poziom trudności (1-10): ", 1, 10, "Nieprawidłowy poziom trudności! Upewnij się, że wprowadziłeś liczbę w odpowienim przedziale!");
    }
    public int PobierzIloscCwiczen()
    {
        return PobierzLiczbe("Podaj ilość ćwiczeń: ", 1, 50, "Ilość ćwiczeń nie może być równa 0!");
    }
    public List<Cwiczenie>? PobierzCwiczenia()
    {
        int ilosc = PobierzIloscCwiczen();
        if(ilosc == -1)
        {
            return null;
        }
        List<Cwiczenie> cwiczenia = new List<Cwiczenie>();
        for(int i = 1; i <= ilosc; i++)
        {
            string nazwaCwiczenia = PobierzTekst("Podaj nazwę ćwiczenia #" + i + ": ", "Upewnij się, że podałeś nazwę!");
            if(nazwaCwiczenia == null)
            {
                return null;
            }
            int serie = PobierzSerie(i);
            if(serie == -1)
            {
                Console.WriteLine("Upewnij się że podałeś ilość serii większą od zera!");
                return null;
            }
            int powtorzenia = PobierzPowtorzenia(i);
            if(powtorzenia == -1)
            {
                Console.WriteLine("Upewnij się że podałeś ilość powtorzeń większą od zera!");
                return null;
            }
            int przerwa = PobierzPrzerweMiedzySeriami(i);
            if(przerwa == -1)
            {
                Console.WriteLine("Upewnij się że podałeś poprawna długość przerwy!");
                return null;
            }
            cwiczenia.Add(new Cwiczenie(nazwaCwiczenia, serie, powtorzenia, przerwa));
        } 
        return cwiczenia;
    }
    public int PobierzSerie(int i)
    {
        return PobierzLiczbe("Podaj ilość serii dla ćwiczenia #" + i + ": ", 1, 20, "Musisz podac liczbe wieksza od zera!");
    }
    public int PobierzPowtorzenia(int i)
    {
        return PobierzLiczbe("Podaj ilość powtórzeń dla ćwiczenia #" + i + ": ", 1, 50, "Musisz podać liczbe większą od zera!");
    }
    public int PobierzPrzerweMiedzySeriami(int i)
    {
        return PobierzLiczbe("Podaj przerwe miedzy seriami dla cwiczenia #" + i + ": ", 0, 800, "Musisz podać liczbe! Od 0 w górę!");
    }
    public int WezId()
    {
        return PobierzLiczbe("Podaj ID treningu, który chcesz zacząć: ", 1, 100, "Nieprawidłowy format ID. Upewnij się że wprowadziłeś liczbę większą od 0!");
    }
}