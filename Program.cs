using System;
using System.Collections.Generic;
using System.Numerics;
using TreningApp.Models;
using TreningApp.UI;

namespace TreningApp;

class Program
{   
    static PlanService planService = new PlanService();
    static TreningService treningService = new TreningService();
    static ConsoleRenderer renderer = new ConsoleRenderer(); 
    static InputHelper inputHelper = new InputHelper();
    static HistoriaService historiaService = new HistoriaService();
    static bool isRunning = true;
    static void Main(string[] args)
    {
        historiaService.OdczytHistorii();
        planService.OdczytPlanow();
        while (isRunning)
        {
            ShowMenu();
            string choice = inputHelper.PobierzTekst("Wybierz opcję: ", "Błędna opcja. Spróbuj ponownie.");
            switch (choice)
            {
                case "1":
                    DodajPlan();
                    break;
                case "2":
                    Console.WriteLine("Twoje treningi: ");
                    Console.WriteLine("-------------------------------------------");
                    foreach(var plan in planService.GetPlany())
                    {
                        renderer.WyswietlPlan(plan);
                    }
                    break;
                case "3":
                    RozpocznijTrening();
                    break;
                case "4":
                    Console.WriteLine("Twoje plany z cwiczeniami: ");
                    Console.WriteLine("===========================================");
                    foreach(var plan in planService.GetPlany())
                    {
                        renderer.WyswietlPlanZCwiczeniami(plan);
                    }
                    break;
                case "5":
                    int idDoUsuniecia = inputHelper.PobierzLiczbe("Podaj Id treningu, który chcesz usunąć: ",1 , 100, "Nie udało się znaleźć takiego treningu!");
                    planService.UsunPlanPoId(idDoUsuniecia);
                    break;
                case "6":
                    EdytujPlan();
                    break;
                case "7":
                    historiaService.WyswietlHistorieTreningu();
                    break;
                case "8":
                    int idDoWyszukania = inputHelper.PobierzLiczbe("Podaj ID planu, który chcesz zobaczyć w historii: ", 1, 100, "Nie ma takiego planu w historii!");
                    historiaService.WyswietlHistoriePlanu(idDoWyszukania);
                    break;
                case "9":
                    FiltrujHistoriePoDacie();
                    break;
                case "10":
                    int idDoUsunieciaHistorii = inputHelper.PobierzLiczbe("Podaj ID planu, który chcesz usunąć: ", 1, 100, "Nie udało się pobrać ID");
                    historiaService.UsunWpisHistoriiPoId(idDoUsunieciaHistorii);
                    break;
                case "11":
                    treningService.WyswietlStatystyki();
                    break;
                case "12":
                    Console.WriteLine("Zamykam aplikację...");
                    isRunning = false;
                    break;
                default:
                    if(choice == null)
                    {
                    break;
                    }
                    Console.WriteLine("Błędna opcja. Spróbuj ponownie.");
                    break;
            }
        }
    }
    static void ShowMenu()
    {
        Console.WriteLine("===========================================");
        Console.WriteLine("Witam, w mojej aplikacji treningowej.");
        Console.WriteLine("1. Dodaj trening");
        Console.WriteLine("2. Wyświetl treningi");
        Console.WriteLine("3. Zacznij trening");
        Console.WriteLine("4. Wyświetl treningi z ćwiczeniami");
        Console.WriteLine("5. Usuń plan po ID.");
        Console.WriteLine("6. Edytuj plan");
        Console.WriteLine("7. Wyswietl historie treningow");
        Console.WriteLine("8. Wyświetl plan po id");
        Console.WriteLine("9. Wyświetl historie z konkretnego przedziału.");
        Console.WriteLine("10. Usuń wpis z historii");
        Console.WriteLine("11. Statystyki");
        Console.WriteLine("12. Zamknij aplikację");
        Console.WriteLine("===========================================");
    }
    static void DodajPlan()
    {
        string nazwaPlanu = inputHelper.PobierzTekst("Podaj nazwę treningu: ", "Upewnij się że podałeś odpowieni tekst");
        if(nazwaPlanu == null)
        {
            return;
        }
        int poziom = inputHelper.PobierzPoziom();
        if(poziom == -1)
        {
            return;
        }
        string rodzajTreningu = inputHelper.PobierzTekst("Podaj rodzaj treningu (np. siłowy, cardio): ", "Upewnij się, że podałeś odpowieni tekst");
        if(rodzajTreningu == null)
        {
            return;
        }
        int iloscObwodow = inputHelper.PobierzIloscObwodow(1);
        if(iloscObwodow == -1)
        {
            return;
        }
        int przerwaO = inputHelper.PobierzPrzerweMiedzyObwodami(iloscObwodow);
        if(przerwaO == -1)
        {
            Console.WriteLine("Nie udało się pobrać przerwy miedzy obwodami!\nUpewnij się że podałeś odpowieną długość przerwy!");
            return;
        }
        List<Cwiczenie> listaCwiczen = inputHelper.PobierzCwiczenia();
        if(listaCwiczen == null)
        {
            Console.WriteLine("Nie udało się pobrać listy cwiczen!\nTworzenie planu zostało przerwane.");
            return;
        }
        planService.DodajPlan(nazwaPlanu, poziom, rodzajTreningu, iloscObwodow, przerwaO, listaCwiczen);
    }
    static void RozpocznijTrening()
    {
        int id = inputHelper.WezId();
        if(id == -1)
        {
            return; // Jeśli ID jest nieprawidłowe, zakończ funkcję i wróć do menu
        }
        Plan aktualnyPlan = planService.ZnajdzPlanPoId(id);
        if(aktualnyPlan != null)
        {
            treningService.WykonajTreningZWynikiem(aktualnyPlan);
        }else
        {
            Console.WriteLine("Nie znaleziono treningu o podanym id. Upewnij się, że podałeś poprawne ID.");
        }
    }
    static void FiltrujHistoriePoDacie()
    {
        Console.Write("Podaj datę OD (dd.MM.yyy): ");
        string odInput = Console.ReadLine();
        if(!DateTime.TryParse(odInput, out DateTime od))
        {
            Console.WriteLine("Nie podano poprawnej daty!");
            return;
        }
        Console.Write("Podaj datę Do (dd.MM.yyy): ");
        string doInput = Console.ReadLine();
        if(!DateTime.TryParse(doInput, out DateTime doKiedy))
        {
            Console.WriteLine("Nie podano poprawnej daty!");
            return;
        }
        if(od > doKiedy)
        {
            Console.WriteLine("Prawdopodobnie podałeś datę odwrotnie. Upewnij się że są one poprawne!");
            return;
        }
        historiaService.WyswietlHistorieZData(od, doKiedy);
    }
    static void EdytujPlan()
    {
        int idDoEdycji = inputHelper.PobierzLiczbe("Podaj ID treningu do edycji: ", 1, 100, "Nie udało się znaleźć planu o takim ID.");
            if(idDoEdycji == -1)
            {   
                return;
            }
            Plan planDoEdycji = planService.ZnajdzPlanPoId(idDoEdycji);
            if(planDoEdycji == null)
            {
                Console.WriteLine("Nie ma takiego planu!");
                return;
            }
            string nowaNazwa = inputHelper.PobierzTekst("Podaj nową nazwę: ", "Upewnij się, że podałeś odpowiedni tekst");
            if(nowaNazwa == null)
            {
                return;
            }
            int nowyPoziom = inputHelper.PobierzLiczbe("Podaj nowy poziom (1, 10): ", 1, 10, "Upewnij się że podałeś liczbę w odpowienim przedziale!");
            if(nowyPoziom == -1)
            {
                return;
            }
            string nowyRodzaj = inputHelper.PobierzTekst("Podaj nowy rodzaj: ", "Upewnij się że podałeś dobrą nazwę");
            if(nowyRodzaj == null)
            {
                return;
            }
            int nowaIloscObwodow = inputHelper.PobierzLiczbe("Podaj nowa ilosc obwodow: ", 0, 50, "Upewnij się że podałeś liczbę.");
            if(nowaIloscObwodow == -1)
            {
                return;
            }
            int nowaPrzerwaMiedzyObwodami = inputHelper.PobierzLiczbe("Podaj nową długość przerwy: ", 0, 1000, "Upewnij się że podałeś liczbę.");
            if(nowaPrzerwaMiedzyObwodami == -1)
            {
                return;
            }
            planService.EdytujPlanPoId(planDoEdycji, nowaNazwa, nowyPoziom, nowyRodzaj, nowaIloscObwodow, nowaPrzerwaMiedzyObwodami);
        
    }
}