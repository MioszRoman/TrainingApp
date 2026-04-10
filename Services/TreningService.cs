using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Linq;

namespace TreningApp;

class TreningService
{
    private List<SesjaTreningowa> listaTreningow = new List<SesjaTreningowa>();
    private List<SesjaObwodu> listaObwodow = new List<SesjaObwodu>();
    private List<SesjaSerii> listaSerii = new List<SesjaSerii>();
    private List<HistoriaTreningu> historia = new List<HistoriaTreningu>();
    private string sciezkaHistorii = Path.Combine("Data", "Historia.json");

    private JsonSerializerOptions JsonOptions()
    {
        var options = new JsonSerializerOptions();
        options.TypeInfoResolver = new DefaultJsonTypeInfoResolver();
        options.WriteIndented = true;
        return options;
    }
    public void ZapisHisorii()
    {
        string jsonString = JsonSerializer.Serialize(historia, JsonOptions());
        File.WriteAllText(sciezkaHistorii, jsonString);
        //Console.WriteLine("Zapisano pomyślnie do pliku Plany.json");
    }
    public void OdczytHistorii()
    {
        if(!File.Exists(sciezkaHistorii))
        {
            return;
        }
        string zapis = File.ReadAllText(sciezkaHistorii);
        if(string.IsNullOrWhiteSpace(zapis))
        {
            historia = new List<HistoriaTreningu>();
            return;
        }
        var des = JsonSerializer.Deserialize<List<HistoriaTreningu>>(zapis, JsonOptions());
        if(des != null)
        {
            historia = des;
        }
    }
    public void UsunWpisHistoriiPoId(int id)
    {
        if(historia.Count == 0)
        {
            Console.WriteLine("Brak historii treningów.");
            return;
        }
        int ileUsunieto = historia.RemoveAll(h => h.IdPlanu == id);
        if(ileUsunieto == 0)
        {
            Console.WriteLine("Nie usunięto żadnego planu.");
        }
        else
        {
            ZapisHisorii();
            Console.WriteLine($"Pomyślnie usunięto {ileUsunieto} wpisów.");
        }
    }
    public void WyswietlHistorieTreningu()
    {
        if(historia.Count == 0)
            {
                Console.WriteLine("Brak historii treningów.");
                return;
            }else
            {
            Console.WriteLine("Historia Treningów:");
            }
        var posortowane = historia.OrderByDescending(h => h.DataTreningu).ToList();
        foreach(var historiaPlanu in posortowane)
        {
                WyswietlHistorie(historiaPlanu);
        }
    }
    public void WyswietlHistorie(HistoriaTreningu historiaPlanu)
    {
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine($"ID treningu:  {historiaPlanu.IdPlanu}");
        Console.WriteLine("Nazwa planu: " + historiaPlanu.NazwaPlanu);
        Console.WriteLine("Data treningu: " + historiaPlanu.DataTreningu.ToString("dd.MM.yyyy HH:mm"));
        int wszystkieSekundy = (int)historiaPlanu.CzasTrwania;
        int minuty = wszystkieSekundy / 60;
        int sekundy = wszystkieSekundy % 60;
        Console.WriteLine("Czas trwania: " + minuty + " min " + sekundy.ToString("00") + "s");
        Console.WriteLine("-------------------------------------------");
    }
    public void WyswietlHistorieZData(DateTime od, DateTime doKiedy)
    {
        if(historia.Count == 0)
        {
            Console.WriteLine("Brak historii treningów!");
            return;
        }
        Console.WriteLine($"Historia od {od:dd.MM.yyyy} do {doKiedy:dd.MM.yyyy}.");
        bool czyZnalezionoPlan = false;
        foreach(var historiaPlanu in historia)
        {
            if(historiaPlanu.DataTreningu.Date >= od.Date && historiaPlanu.DataTreningu.Date <= doKiedy.Date)
            {
                WyswietlHistorie(historiaPlanu);
                czyZnalezionoPlan = true;
            }
        }
        if(czyZnalezionoPlan == false)
        {
            Console.WriteLine("Nie znaleziono historii w podanym zakresie!");
            return;
        }
    }
    public void WyswietlHistoriePlanu(int id)
    {
        if(historia.Count == 0)
        {
            Console.WriteLine("Brak historii treningów!");
            return;
        }
        bool czyZnaleziono = false;
        foreach(var historiaPlanu in historia)
        {
            if(historiaPlanu.IdPlanu == id)
            {
                
                WyswietlHistorie(historiaPlanu);
                czyZnaleziono = true;
            }
        }
        if(czyZnaleziono == false)
            {
            Console.WriteLine("Nie znaleziono takiego planu!");
            return;
            }
    }
    public string WyswietlenieCzasu(double calyCzas)
    {
        int wszystkieSekundy = (int)calyCzas;
        int minuty = wszystkieSekundy / 60;
        int sekundy = wszystkieSekundy % 60;
        string komunikat = $"{minuty} min {sekundy} s";
        return komunikat;
    }
    public void WyswietlStatystyki()
    {
        //Ilość treningów 
        if(historia.Count == 0)
        {
            Console.WriteLine("Brak historii treningów.");
            return;
        }
        Console.WriteLine($"Ilość treningów: {historia.Count}");
        LacznyCzas();
        SredniCzas();
        NajczesciejWykonywanyTrening();
        PoszczegolneWykonania();
        NajdluzszyTrening();
        NajkrotszyTrening();
        NajdluzszyWTygodniu();        
    }
    public void NajdluzszyTrening()
    {
        var najdluzszy = historia
        .OrderByDescending(x => x.CzasTrwania)
        .First();
        Console.WriteLine($"Najdłuższy trening: {najdluzszy.NazwaPlanu} trwał {WyswietlenieCzasu(najdluzszy.CzasTrwania)}");
    }
    public void NajkrotszyTrening()
    {
        var najkrotszy = historia.MinBy(x => x.CzasTrwania);
        Console.WriteLine($"Najkrotszy trening to: {najkrotszy.NazwaPlanu} trwał {WyswietlenieCzasu(najkrotszy.CzasTrwania)}");
    }
    public double WszystkieSekundy()
    {
        double lacznyCzas = 0;
        foreach(var czas in historia)
        {
            lacznyCzas += czas.CzasTrwania;
        }
        return lacznyCzas;
    }
    public void SredniCzas()
    {
        double czas = WszystkieSekundy();
        string sredniCzas = WyswietlenieCzasu((czas / historia.Count));    
        Console.WriteLine($"Średni czas treningów {sredniCzas}");
    
    }
    public void LacznyCzas()
    {
        double czas = WszystkieSekundy();
        string ladnyCzas = WyswietlenieCzasu(czas);
        Console.WriteLine($"Łączny czas treningów {ladnyCzas}");

    }
    public void NajczesciejWykonywanyTrening()
    {
        var najczestszy = historia
        .GroupBy(x => x.NazwaPlanu) //Grupujemy identyczne elementy
        .OrderByDescending(g => g.Count()) //Sortujemy po liczbie wystąpień
        .Select(g => g.Key) //Wybieramy nazwę elementu (klucz)
        .FirstOrDefault(); //Wynik
        Console.WriteLine($"Najczęściej występujący trening to: {najczestszy}");

    }
    public void PoszczegolneWykonania()
    {
        var grupy = historia
        .GroupBy(x => x.NazwaPlanu)
        .OrderByDescending(g => g.Count());
        Console.WriteLine("Poszczególne wystąpienia treningów: ");
        foreach(var grupa in grupy)
        {
            Console.WriteLine($"{grupa.Key} -> {grupa.Count()}");
        }
    }

    public void NajdluzszyWTygodniu()
    {
        DateTime dzisiaj = DateTime.Now;
        DateTime ostatniTydzien = dzisiaj.AddDays(-7);
        var historiaZTygodnia = historia.Where(x => x.DataTreningu >= ostatniTydzien);
        if(historiaZTygodnia.Count() == 0)
        {
            Console.WriteLine("Nie ma treningów w ostanim tygodniu.");
            return;
        }
        var najdluzszyWTygodniu = historiaZTygodnia.MaxBy(x => x.CzasTrwania);
        Console.WriteLine($"Najdłuższy trening w ostatnim tygodniu to: {najdluzszyWTygodniu.NazwaPlanu} trwał on: {WyswietlenieCzasu(najdluzszyWTygodniu.CzasTrwania)}");
    }
    public void WyswietlPodsumowanieAktualnegoTreningu()
    {
        Console.WriteLine("Podsumowanie treningu: ");
        Console.WriteLine("===========================================");
        foreach(var trening in listaTreningow)
        {
            trening.WyswietlSesje();
            foreach(var obwod in listaObwodow)
            {
                obwod.WyswietlSesjeObwodu();
                foreach(var seria in listaSerii)
                {
                    if(seria.NumerObwodu == obwod.NumerObwodu) // Wyświetl tylko serie, które należały do danego obwodu
                    {
                    seria.WyswietlSesjeSerii();
                    }
                }   
            }
        }
    }
    private void Odliczanie(int sekundy, string komunikat)
    {
        for(int j = 0; j < sekundy; j++)
                    {
                        Console.Write("\r"+ komunikat + (sekundy - j) + " s. ");
                        Thread.Sleep(1000); // Jeśli przerwa jest większa niż 0, odczekaj 1 sekundę
                    }
                    Console.WriteLine();
    }
    private void WykonanieCwiczenWObwodzie(Plan plan, int obwod)
    {
        foreach(var cwiczenie in plan.Cwiczenia)
        {
            Console.WriteLine("Wykonujesz ćwiczenie: " + cwiczenie.NazwaCwiczenia);
            //Console.WriteLine("Serii: " + cwiczenie.LiczbaSerii);
            for(int i = 1; i <= cwiczenie.LiczbaSerii; i++)
            {
                DateTime startSerii = DateTime.Now;
                Console.WriteLine("Wykonujesz serię " + i + " z " + cwiczenie.LiczbaSerii);
                Console.WriteLine("Powtórzenia: " + cwiczenie.LiczbaPowtorzen);
                Console.WriteLine("Po zakończeniu ćwiczenia naciśnij Enter, aby przejść do przerwy...");
                while(Console.ReadKey().Key != ConsoleKey.Enter) {/*Czekaj na wciśnięcie klawisza Enter*/}
                DateTime stopSerii = DateTime.Now;
                TimeSpan czasTrwaniaSerii = stopSerii - startSerii;
                listaSerii.Add(new SesjaSerii(plan, cwiczenie, i, startSerii, czasTrwaniaSerii, obwod));
                //Console.WriteLine("Przerwa miedzy seriami: " + cwiczenie.PrzerwaMiedzySeriami + " s.");
                if (cwiczenie != plan.Cwiczenia[^1] || i != cwiczenie.LiczbaSerii) // Jeśli to nie jest ostatnia seria i nie jest to ostatnie ćwiczenie w obwodzie
                {
                    Odliczanie(cwiczenie.PrzerwaMiedzySeriami, "Przerwa miedzy seriami: ");
                }
            }
            Console.WriteLine("---------------------------------------------------------------");
        }
    }
    private void WykonanieObwodu(Plan planDoZaczecia)
    {
        for(int obwod = 1; obwod <= planDoZaczecia.IloscObwodow; obwod++)
        {
            DateTime startObwodu = DateTime.Now;
            Console.WriteLine("Obwód " + obwod + " z " + planDoZaczecia.IloscObwodow);
            WykonanieCwiczenWObwodzie(planDoZaczecia, obwod);
            DateTime stopObwodu = DateTime.Now;
            TimeSpan czasTrwaniaObwodu = stopObwodu - startObwodu;
            listaObwodow.Add(new SesjaObwodu(planDoZaczecia, startObwodu, czasTrwaniaObwodu, obwod));
            if(obwod != planDoZaczecia.IloscObwodow && planDoZaczecia.PrzerwaMiedzyObwodami > 0) // Jeśli to nie jest ostatni obwód
            {
                Odliczanie(planDoZaczecia.PrzerwaMiedzyObwodami, "Przerwa miedzy obwodami: ");
            }
        }
    }
    public void WykonajTreningZWynikiem(Plan planDoZaczecia)
    {
        listaTreningow.Clear();
        listaObwodow.Clear();
        listaSerii.Clear();
        Console.WriteLine("Zaczynasz trening: " + planDoZaczecia.Nazwa);
        DateTime startTreningu = DateTime.Now;
        WykonanieObwodu(planDoZaczecia);
        Console.WriteLine("Trening zakończony! Świetna robota, starx!");
        DateTime stopTreningu = DateTime.Now;
        TimeSpan czasTrwaniaTreningu = stopTreningu - startTreningu;
        listaTreningow.Add(new SesjaTreningowa(planDoZaczecia, startTreningu, czasTrwaniaTreningu));
        WyswietlPodsumowanieAktualnegoTreningu();
        historia.Add(new HistoriaTreningu(planDoZaczecia.Id, planDoZaczecia.Nazwa, startTreningu, czasTrwaniaTreningu.TotalSeconds));
        ZapisHisorii();

    }
}