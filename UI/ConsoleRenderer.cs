using System.Runtime.Serialization;
using TreningApp.Models;

namespace TreningApp.UI;

public class ConsoleRenderer
{
    public void PokazKomunikat(string komunikat)
    {
        Console.WriteLine(komunikat);
    }
    public void PokazKomunikatBezNowejLinii(string komunikat)
    {
        Console.Write(komunikat);
    }
    public void PokazSukces(string komunikat)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(komunikat);
        Console.ResetColor();
    }
    public void PokazBlad(string komunikat)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(komunikat);
        Console.ResetColor();
    }
    public void PokazNaglowek(string tekst)
    {
        Console.WriteLine($"\n===== {tekst.ToUpper()} =====");
    }
    public void Separator()
    {
        Console.WriteLine(new string('-', 40));
    }
    public void MocnySeparator()
    {
        Console.WriteLine(new string('=', 40));
    }
    public void WyswietlPlan(Plan plan)
    {
        Console.WriteLine($"ID: {plan.Id}");
        Console.WriteLine($"Nazwa: {plan.Nazwa}");
        Console.WriteLine($"Poziom: {plan.Poziom}");
        Console.WriteLine($"Rodzaj: {plan.Rodzaj}");
        Console.WriteLine($"Liczba obwodów: {plan.IloscObwodow}");
        Console.WriteLine($"Przerwa między obwodami: {plan.PrzerwaMiedzyObwodami}");
    }
    public void WyswietlPlanZCwiczeniami(Plan plan)
    {
        WyswietlPlan(plan);
        if(plan.Cwiczenia != null && plan.Cwiczenia.Count > 0)
        {
            Console.WriteLine("Ćwiczenia: ");
            foreach (var cwiczenie in plan.Cwiczenia)
            {
                WyswietlCwiczenie(cwiczenie);
            }
        }
        Console.WriteLine(new string ('-', 30));
    }

    public void WyswietlCwiczenie(Cwiczenie cwiczenie)
    {
        Console.WriteLine($"- {cwiczenie.NazwaCwiczenia}: {cwiczenie.LiczbaSerii} serie, {cwiczenie.LiczbaPowtorzen} powtórzeń, {cwiczenie.PrzerwaMiedzySeriami} s. przerwy.");
    }

    public void WyswietlSesjeObwodu(SesjaObwodu obwod)
    {
        Console.WriteLine("===========================================");
        Console.WriteLine("Obwod: " + obwod.NumerObwodu);
        Console.WriteLine("Data rozpoczęcia obwodu: " + obwod.DataRozpoczecia);
        Console.WriteLine("Czas trwania obwodu: " + obwod.CzasTrwaniaObwodu.TotalSeconds + " sekund");
        Console.WriteLine("===========================================");
    }
    public void WyswietlSesjeSerii(SesjaSerii seria)
    {
        Console.WriteLine("===========================================");
        Console.WriteLine("Obwod: " + seria.NumerObwodu);
        Console.WriteLine("Ćwiczenie: " + seria.Cwiczenie.NazwaCwiczenia);
        Console.WriteLine("Seria: " + seria.NumerSerii + " z " + seria.Cwiczenie.LiczbaSerii);
        Console.WriteLine("Data rozpoczęcia serii: " + seria.DataRozpoczeciaSerii);
        Console.WriteLine("Czas trwania serii: " + seria.CzasTrwaniaSerii.TotalSeconds + " sekund");
        Console.WriteLine("===========================================");
    }
    public void WyswietlSesje(SesjaTreningowa sesja)
    {
        Console.WriteLine("===========================================");
        Console.WriteLine("Plan treningowy: " + sesja.PlanTreningowy.Nazwa);
        Console.WriteLine("Data treningu: " + sesja.DataTreningu);
        Console.WriteLine("Czas trwania treningu: " + sesja.CzasTrwaniaTreningu.TotalSeconds + " sekund");
        Console.WriteLine("===========================================");
    }
    public string FormatujCzas(double calyCzas)
    {
        int wszystkieSekundy = (int)calyCzas;
        int minuty = wszystkieSekundy / 60;
        int sekundy = wszystkieSekundy % 60;
        string komunikat = $"{minuty} min {sekundy} s";
        return komunikat;
    }
    public void WyswietlWpisHistorii(HistoriaTreningu historiaPlanu)
    {
        Separator();
        Console.WriteLine($"ID treningu:  {historiaPlanu.IdPlanu}");
        Console.WriteLine("Nazwa planu: " + historiaPlanu.NazwaPlanu);
        Console.WriteLine("Data treningu: " + historiaPlanu.DataTreningu.ToString("dd.MM.yyyy HH:mm"));
        string komunikat = FormatujCzas(historiaPlanu.CzasTrwania);
        Console.WriteLine($"Czas trwania: {komunikat}");
        Separator();
    }
    public void WyswietlHistorie(List<HistoriaTreningu> wpisy)
    {
        if(wpisy == null || wpisy.Count == 0)
        {
            PokazKomunikat("Brak historii treningów.");
            return;
        }
        PokazNaglowek("Historia Treningów:");
        foreach(var wpis in wpisy)
        {
            WyswietlWpisHistorii(wpis);
        }
    }
    public void WyswietlWynikTreningu(WynikTreningu wynik)
    {
        PokazKomunikat($"Nazwa treningu: {wynik.Nazwa}");
        PokazKomunikat($"Trwał: {FormatujCzas(wynik.Czas)}");
        PokazKomunikat($"Odbył się: {wynik.Data.Date}");
    }
    public void WyswietlWykonanie(Wykonanie wykonania)
    {
        PokazKomunikat($"Nazwa: {wykonania.Nazwa}");
        PokazKomunikat($"Odbył się: {wykonania.Ilosc} razy");
    }
    public void WyswietlStatystyki(Statystyki statystyki)
    {
        PokazNaglowek("Statystyki");
        PokazKomunikat($"Ilość treningów: {statystyki.IloscTreningow}");
        Separator();
        PokazKomunikat($"Łączny czas treningow: {FormatujCzas(statystyki.LacznyCzas)}");
        Separator();
        PokazKomunikat($"Średni czas treningów: {FormatujCzas(statystyki.SredniCzas)}");
        Separator();
        PokazKomunikat("Najdłuższy trening to:");
        if(statystyki.NajdluzszyTrening == null)
        {
            PokazKomunikat("Brak informacji.");
        }
        else
        {
            WyswietlWynikTreningu(statystyki.NajdluzszyTrening);
        }
        Separator();
        PokazKomunikat("Najkrótszy trening to:");
        if(statystyki.NajkrotszyTrening == null)
        {
            PokazKomunikat("Brak informacji.");
        }
        else
        {
            WyswietlWynikTreningu(statystyki.NajkrotszyTrening);
        }
        Separator();
        PokazKomunikat("Najdłuższy trening w tym tygodniu to:");
        if(statystyki.NajdluzszyTreningWTygodniu == null)
        {
            PokazKomunikat("Brak treningów w ostatnim tygodniu");
        }
        else
        {
        WyswietlWynikTreningu(statystyki.NajdluzszyTreningWTygodniu);
        }
        Separator();
        PokazKomunikat("Najczęstszym treningiem był:");
        if(statystyki.Najczestszy == null)
        {
            PokazKomunikat("Brak informacji.");
        }
        else
        {
            WyswietlWykonanie(statystyki.Najczestszy);
        }
        Separator();
        PokazKomunikat("Poszczegolne wykonania wszystkich treningów:");
        foreach(var wykonanie in statystyki.PoszczegolneWykonania)
        {
            WyswietlWykonanie(wykonanie);
        }
    }
}