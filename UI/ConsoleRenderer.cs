using TreningApp.Models;

namespace TreningApp.UI;

public class ConsoleRenderer
{

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
}