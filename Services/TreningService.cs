using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Linq;
using TreningApp.Models;
using TreningApp.UI;

namespace TreningApp;

class TreningService
{
    private List<SesjaTreningowa> listaTreningow = new List<SesjaTreningowa>();
    private List<SesjaObwodu> listaObwodow = new List<SesjaObwodu>();
    private List<SesjaSerii> listaSerii = new List<SesjaSerii>();
    private ConsoleRenderer renderer = new ConsoleRenderer();
    public void WyswietlPodsumowanieAktualnegoTreningu()
    {
        Console.WriteLine("Podsumowanie treningu: ");
        Console.WriteLine("===========================================");
        foreach(var trening in listaTreningow)
        {
            renderer.WyswietlSesje(trening);
            foreach(var obwod in listaObwodow)
            {
                renderer.WyswietlSesjeObwodu(obwod);
                foreach(var seria in listaSerii)
                {
                    if(seria.NumerObwodu == obwod.NumerObwodu) // Wyświetl tylko serie, które należały do danego obwodu
                    {
                    renderer.WyswietlSesjeSerii(seria);
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
    public void WykonajTreningZWynikiem(Plan planDoZaczecia, HistoriaService historiaService)
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
        historiaService.ZapiszHistorie(planDoZaczecia, startTreningu, czasTrwaniaTreningu);

    }
}