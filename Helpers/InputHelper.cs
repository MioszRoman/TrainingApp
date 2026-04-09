using System;
using System.Collections.Generic;

namespace TreningApp;

class InputHelper
{
    public int PobierzLiczbe(string komunikat, int liczbaMin, int liczbaMax, string blad)
    {
        Console.Write(komunikat);
        bool czyLiczba = int.TryParse(Console.ReadLine(), out int liczba);
        if(!czyLiczba)
        {
            Console.WriteLine(blad);
            return -1;
        }
        if(liczba < liczbaMin)
        {
            Console.WriteLine(blad);
            return -1;
        }
        if(liczba > liczbaMax)
        {
            Console.WriteLine(blad);
            return -1;
        }
        return liczba;
    }
    public string PobierzTekst(string komunikat, string blad)
    {
        Console.Write(komunikat);
        string text = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine(blad);
            return null;
        }
        return text;
    }
    public int PobierzIloscObwodow(int liczbaMin)
    {
        return PobierzLiczbe("Podaj ilość obwodów: ", liczbaMin, 20, "Upewnij się, że podałeś liczbę większą od zera!");
        /*Console.Write("Podaj ilość obwodów: ");
        bool czyIloscObwodow = int.TryParse(Console.ReadLine(), out int result);
        if(!czyIloscObwodow || result <= 0)
        {
            Console.WriteLine("Upewnij się że podałeś liczbę większą od zera!");
            return 0;
        }
        return result;*/
    }
    public int PobierzPrzerweMiedzyObwodami(int iloscObwodow)
    {
        int przerwaO = 0;
        if(iloscObwodow >= 2){
            return PobierzLiczbe("Podaj przerwę między obwodami (w sekundach): ", 0, 800, "Przerwa powinna być liczbą! Od 0 w górę!");
            /*
            Console.Write("Podaj przerwę między obwodami (w sekundach): ");
            bool czyPrzerwaMiedzyObwodami = int.TryParse(Console.ReadLine(), out przerwaO);
            if(!czyPrzerwaMiedzyObwodami || przerwaO < 0)
            {
                Console.WriteLine("Przerwa powinna być liczba! Od 0 w góre!");
                return -1;
            }*/
        }
        return przerwaO;
    }
    public int PobierzPoziom()
    {
        return PobierzLiczbe("Podaj poziom trudności (1-10): ", 1, 10, "Nieprawidłowy poziom trudności! Upewnij się, że wprowadziłeś liczbę w odpowienim przedziale!");
        /*Console.Write("Podaj poziom trudnosci (1-10): ");
        bool poziomTrudnosci = int.TryParse(Console.ReadLine() , out int poziom);
        if(!poziomTrudnosci || poziom < 1 || poziom > 10)
        {
            Console.WriteLine("Nieprawidłowy poziom trudności. Upewnij się, że wprowadziłeś liczbę od 1 do 10.");
            return 0; // Jeśli poziom trudności jest nieprawidłowy, zakończ funkcję i wróć do menu
        }
        return poziom;*/
    }
    public int PobierzIloscCwiczen()
    {
        return PobierzLiczbe("Podaj ilość ćwiczeń: ", 1, 50, "Ilość ćwiczeń nie może być równa 0!");
    }
    public List<Cwiczenie> PobierzCwiczenia()
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
        /*Console.Write("Podaj ilość serii dla ćwiczenia #" + i + ": ");
            bool czySerie = int.TryParse(Console.ReadLine(), out int liczbaSerii);
            if (!czySerie || liczbaSerii <= 0)
            {
                Console.WriteLine("Musisz podac liczbe wiekszą od zera!");
                return 0;
            }
            return liczbaSerii;*/
    }
    public int PobierzPowtorzenia(int i)
    {
        return PobierzLiczbe("Podaj ilość powtórzeń dla ćwiczenia #" + i + ": ", 1, 50, "Musisz podać liczbe większą od zera!");
        /*Console.Write("Podaj ilość powtórzeń dla ćwiczenia #" + i + ": ");
            bool czyPowtorzenia = int.TryParse(Console.ReadLine(), out int liczbaPowtorzen);
            if(!czyPowtorzenia || liczbaPowtorzen <= 0)
            {
                Console.WriteLine("Musisz podać liczbe wieksza od zera!");
                return 0;
            }
            return liczbaPowtorzen;*/
    }
    public int PobierzPrzerweMiedzySeriami(int i)
    {
        return PobierzLiczbe("Podaj przerwe miedzy seriami dla cwiczenia #" + i + ": ", 0, 800, "Musisz podać liczbe! Od 0 w górę!");
        /*Console.Write("Podaj przerwe miedzy seriami dla cwiczenia #" + i + ": ");
            bool czyPrzerwa = int.TryParse(Console.ReadLine(), out int przerwaMiedzySeriami);
            if(!czyPrzerwa || przerwaMiedzySeriami < 0)
            {
                Console.WriteLine("Musisz podac liczbe! Od 0 w górę!");
                return -1;
            }
            return przerwaMiedzySeriami;*/
    }
    public int WezId()
    {
        return PobierzLiczbe("Podaj ID treningu, który chcesz zacząć: ", 1, 100, "Nieprawidłowy format ID. Upewnij się że wprowadziłeś liczbę większą od 0!");
        /*Console.Write("Podaj Id treningu, który chcesz zacząć: ");
        bool idTreningu = int.TryParse(Console.ReadLine(), out int id);
        if(!idTreningu)
        {
            Console.WriteLine("Nieprawidłowy format ID. Upewnij się, że wprowadziłeś liczbę.");
            return null;
        }
        if(id <= 0)
        {
            Console.WriteLine("ID musi być większe od 0. Spróbuj ponownie.");
            return null;
        }
        return id;*/
    }
}