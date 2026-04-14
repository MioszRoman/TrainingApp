using System;
using System.Collections.Generic;
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
        renderer.PokazNaglowek("Podsumowanie treningu: ");
        renderer.MocnySeparator();
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
                        renderer.PokazKomunikat("\r"+ komunikat + (sekundy - j) + " s. ");
                        Thread.Sleep(1000); // Jeśli przerwa jest większa niż 0, odczekaj 1 sekundę
                    }
                    renderer.PokazKomunikat("`");
    }
    private void WykonanieCwiczenWObwodzie(Plan plan, int obwod)
    {
        foreach(var cwiczenie in plan.Cwiczenia)
        {
            renderer.PokazKomunikat("Wykonujesz ćwiczenie: " + cwiczenie.NazwaCwiczenia);
            for(int i = 1; i <= cwiczenie.LiczbaSerii; i++)
            {
                DateTime startSerii = DateTime.Now;
                renderer.PokazKomunikat("Wykonujesz serię " + i + " z " + cwiczenie.LiczbaSerii);
                renderer.PokazKomunikat("Powtórzenia: " + cwiczenie.LiczbaPowtorzen);
                renderer.PokazKomunikat("Po zakończeniu ćwiczenia naciśnij Enter, aby przejść do przerwy...");
                while(Console.ReadKey().Key != ConsoleKey.Enter) {/*Czekaj na wciśnięcie klawisza Enter*/}
                DateTime stopSerii = DateTime.Now;
                TimeSpan czasTrwaniaSerii = stopSerii - startSerii;
                listaSerii.Add(new SesjaSerii(plan, cwiczenie, i, startSerii, czasTrwaniaSerii, obwod));
                if (cwiczenie != plan.Cwiczenia[^1] || i != cwiczenie.LiczbaSerii) // Jeśli to nie jest ostatnia seria i nie jest to ostatnie ćwiczenie w obwodzie
                {
                    Odliczanie(cwiczenie.PrzerwaMiedzySeriami, "Przerwa miedzy seriami: ");
                }
            }
            renderer.Separator();
        }
    }
    private void WykonanieObwodu(Plan planDoZaczecia)
    {
        for(int obwod = 1; obwod <= planDoZaczecia.IloscObwodow; obwod++)
        {
            DateTime startObwodu = DateTime.Now;
            renderer.PokazKomunikat("Obwód " + obwod + " z " + planDoZaczecia.IloscObwodow);
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
        renderer.PokazKomunikat("Zaczynasz trening: " + planDoZaczecia.Nazwa);
        DateTime startTreningu = DateTime.Now;
        WykonanieObwodu(planDoZaczecia);
        renderer.PokazSukces("Trening zakończony! Świetna robota, starx!");
        DateTime stopTreningu = DateTime.Now;
        TimeSpan czasTrwaniaTreningu = stopTreningu - startTreningu;
        listaTreningow.Add(new SesjaTreningowa(planDoZaczecia, startTreningu, czasTrwaniaTreningu));
        WyswietlPodsumowanieAktualnegoTreningu();
        historiaService.ZapiszHistorie(planDoZaczecia, startTreningu, czasTrwaniaTreningu);

    }
}