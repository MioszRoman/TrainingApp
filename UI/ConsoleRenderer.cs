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
    public void WyswietlWpisHistorii(HistoriaTreningu historiaPlanu)
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
}