using System;
using TreningApp.Models;
using TreningApp.UI;
using TreningApp.Services;

namespace TreningApp;

class Program
{   
    static AppDbContext context = new AppDbContext();
    static PlanService planService = new PlanService(context);
    static TreningService treningService = new TreningService();
    static ConsoleRenderer renderer = new ConsoleRenderer(); 
    static InputHelper inputHelper = new InputHelper();
    static HistoriaService historiaService = new HistoriaService(context);
    static StatystykiService statystykiService = new StatystykiService();
    static bool isRunning = true;
    static void Main(string[] args)
    {
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
                    WyswietlPlan();
                    break;
                case "3":
                    RozpocznijTrening();
                    break;
                case "4":
                    WyswietlPlanZCwiczeniami();
                    break;
                case "5":
                    UsunPlanPoId();
                    break;
                case "6":
                    EdytujPlan();
                    break;
                case "7":
                    WyswietlWszystkieWpisy();
                    break;
                case "8":
                    WyswietlWpisyPlanu();
                    break;
                case "9":
                    FiltrujHistoriePoDacie();
                    break;
                case "10":
                    UsunWpisHistorii();
                    break;
                case "11":
                    WyswietlStatystyki();
                    break;
                case "12":
                    renderer.PokazKomunikat("Zamykam aplikację...");
                    isRunning = false;
                    break;
                default:
                    renderer.PokazBlad("Błędna opcja. Spróbuj ponownie.");
                    break;
            }
        }
    }
    static void ShowMenu()
    {
        renderer.MocnySeparator();
        renderer.PokazKomunikat("Witam, w mojej aplikacji treningowej.");
        renderer.PokazKomunikat("1. Dodaj trening");
        renderer.PokazKomunikat("2. Wyświetl treningi");
        renderer.PokazKomunikat("3. Zacznij trening");
        renderer.PokazKomunikat("4. Wyświetl treningi z ćwiczeniami");
        renderer.PokazKomunikat("5. Usuń plan po ID.");
        renderer.PokazKomunikat("6. Edytuj plan");
        renderer.PokazKomunikat("7. Wyświetl historię treningów");
        renderer.PokazKomunikat("8. Wyświetl historię planu po id");
        renderer.PokazKomunikat("9. Wyświetl historię z konkretnego przedziału.");
        renderer.PokazKomunikat("10. Usuń wpis z historii");
        renderer.PokazKomunikat("11. Statystyki");
        renderer.PokazKomunikat("12. Zamknij aplikację");
        renderer.MocnySeparator();
    }
    static void WyswietlWpisyPlanu()
    {
        int idDoWyszukania = inputHelper.PobierzLiczbe("Podaj ID planu, który chcesz zobaczyć w historii: ", 1, 100, "Nie ma takiego planu w historii!");
        var wpisyZID = historiaService.PobierzWpisyPlanu(idDoWyszukania);
        if(wpisyZID.Count == 0)
        {
            renderer.PokazBlad("Nie znaleziono takiego planu!");
        }
        else
        {
            renderer.WyswietlHistorie(wpisyZID);
        }
    }
    static void WyswietlWszystkieWpisy()
    {
        var wpisy = historiaService.PobierzWszystkieWpisy();
        renderer.WyswietlHistorie(wpisy);
    }
    static void UsunPlanPoId()
    {
        int idDoUsuniecia = inputHelper.PobierzLiczbe("Podaj Id treningu, który chcesz usunąć: ",1 , 100, "Nie udało się znaleźć takiego treningu!");
        bool wynik = planService.UsunPlanPoId(idDoUsuniecia);
        if(wynik)
        {
            renderer.PokazSukces("Pomyślnie usunięto plan!");
        }
        else
        {
            renderer.PokazBlad("Nie ma planu o takim ID!");
        }
    }
    static void WyswietlPlan()
    {
        renderer.PokazNaglowek("Twoje treningi");
        renderer.Separator();
        foreach(var plan in planService.GetPlany())
        {
            renderer.WyswietlPlan(plan);
        }
    }
    static void WyswietlPlanZCwiczeniami()
    {
        renderer.PokazNaglowek("Twoje plany z ćwiczeniami: ");
        renderer.MocnySeparator();
        foreach(var plan in planService.GetPlany())
        {
            renderer.WyswietlPlanZCwiczeniami(plan);
        }
    }
    static void UsunWpisHistorii()
    {
        int idDoUsunieciaHistorii = inputHelper.PobierzLiczbe("Podaj ID planu, który chcesz usunąć: ", 1, 100, "Nie udało się pobrać ID");
        var usuniecie = historiaService.UsunWpisHistoriiPoId(idDoUsunieciaHistorii);
        if(usuniecie == 0)
        {
            renderer.PokazBlad("Brak historii treningów.");
        }
        else if(usuniecie == -1)
        {
            renderer.PokazBlad("Nie usunięto żadnego planu.");
        }
        else
        {
            renderer.PokazSukces($"Pomyślnie usunięto {usuniecie} wpisów.");
        }
    }
    static void WyswietlStatystyki()
    {
        var wpisyDoStatystyk = historiaService.PobierzWszystkieWpisy();
        Statystyki? statystyki = statystykiService.PobierzStatystyki(wpisyDoStatystyk);
        if(statystyki == null)
        {
            renderer.PokazKomunikat("Brak statystyk");
        }
        else
        {
            renderer.WyswietlStatystyki(statystyki);
        }
    }
    static void DodajPlan()
    {
        string nazwaPlanu = inputHelper.PobierzTekst("Podaj nazwę treningu: ", "Upewnij się że podałeś odpowiedni tekst");
        int poziom = inputHelper.PobierzPoziom();
        string rodzajTreningu = inputHelper.PobierzTekst("Podaj rodzaj treningu (np. siłowy, cardio): ", "Upewnij się, że podałeś odpowiedni tekst");
        int iloscObwodow = inputHelper.PobierzIloscObwodow(1);
        int przerwaO = inputHelper.PobierzPrzerweMiedzyObwodami(iloscObwodow);
        List<Cwiczenie> listaCwiczen = inputHelper.PobierzCwiczenia();
        int noweID = planService.DodajPlan(nazwaPlanu, poziom, rodzajTreningu, iloscObwodow, przerwaO, listaCwiczen);
        renderer.PokazSukces($"Pomyślnie dodano plan! ID: {noweID}");
    }
    static void RozpocznijTrening()
    {
        int id = inputHelper.WezId();
        Plan? aktualnyPlan = planService.ZnajdzPlanPoId(id);
        if(aktualnyPlan != null)
        {
            treningService.WykonajTreningZWynikiem(aktualnyPlan, historiaService);
        }else
        {
            renderer.PokazBlad("Nie znaleziono treningu o podanym ID. Upewnij się, że podałeś poprawne ID.");
        }
    }
    static void FiltrujHistoriePoDacie()
    {
        renderer.PokazKomunikatBezNowejLinii("Podaj datę OD (dd.MM.yyyy): ");
        string? odInput = Console.ReadLine();
        string blad = "Nie podano poprawnej daty";
        if(!DateTime.TryParse(odInput, out DateTime od))
        {
            renderer.PokazBlad(blad);
            return;
        }
        renderer.PokazKomunikatBezNowejLinii("Podaj datę Do (dd.MM.yyyy): ");
        string? doInput = Console.ReadLine();
        if(!DateTime.TryParse(doInput, out DateTime doKiedy))
        {
            renderer.PokazBlad(blad);
            return;
        }
        if(od > doKiedy)
        {
            renderer.PokazBlad("Prawdopodobnie podałeś datę odwrotnie. Upewnij się że są one poprawne!");
            return;
        }
        var wpisyDat = historiaService.PobierzWpisyZDat(od, doKiedy);
        if(wpisyDat.Count == 0)
        {
            renderer.PokazBlad("Nie znaleziono historii w podanym zakresie.");
        }
        else
        {
            renderer.WyswietlHistorie(wpisyDat);
        }
    }
    static void EdytujPlan()
    {
        int idDoEdycji = inputHelper.PobierzLiczbe("Podaj ID treningu do edycji: ", 1, 100, "Nie udało się znaleźć planu o takim ID.");
        Plan? planDoEdycji = planService.ZnajdzPlanPoId(idDoEdycji);
        if(planDoEdycji == null)
        {
            renderer.PokazBlad("Nie ma takiego planu!");
            return;
        }
        renderer.PokazNaglowek("Edytowany plan");
        renderer.WyswietlPlan(planDoEdycji);
        string nowaNazwa = inputHelper.PobierzTekstDoEdycji("Podaj nową nazwę (Enter = bez zmian): ");
        if(string.IsNullOrWhiteSpace(nowaNazwa))
        {
            nowaNazwa = planDoEdycji.Nazwa;
        }
        int? nowyPoziom = inputHelper.PobierzLiczbeDoEdycji("Podaj nowy poziom (1, 10) (Enter = bez zmian): ", 1, 10, "Upewnij się że podałeś liczbę w odpowiednim przedziale!");
        if(nowyPoziom == null)
        {
            nowyPoziom = planDoEdycji.Poziom;
        }
        string nowyRodzaj = inputHelper.PobierzTekstDoEdycji("Podaj nowy rodzaj (Enter = bez zmian): ");
        if(string.IsNullOrWhiteSpace(nowyRodzaj))
        {
            nowyRodzaj = planDoEdycji.Rodzaj;
        }
        int? nowaIloscObwodow = inputHelper.PobierzLiczbeDoEdycji("Podaj nową ilosc obwodów (Enter = bez zmian): ", 0, 50, "Upewnij się że podałeś liczbę.");
        if(nowaIloscObwodow == null)
        {
            nowaIloscObwodow = planDoEdycji.IloscObwodow;
        }
        int? nowaPrzerwaMiedzyObwodami = inputHelper.PobierzLiczbeDoEdycji("Podaj nową długość przerwy (Enter = bez zmian): ", 0, 1000, "Upewnij się że podałeś liczbę.");
        if(nowaPrzerwaMiedzyObwodami == null)
        {
            nowaPrzerwaMiedzyObwodami = planDoEdycji.PrzerwaMiedzyObwodami;
        }
        bool decyzja = inputHelper.Decyzja("Czy chcesz edytować ćwiczenia? (t/n): ", "Podałeś coś innego. Spróbuj ponownie!");
        List<Cwiczenie> cwiczeniaDoDodania = planDoEdycji.Cwiczenia;        
        if(decyzja)
        {
        List<Cwiczenie> listaCwiczenDoDodania = inputHelper.PobierzCwiczenia();
            cwiczeniaDoDodania = listaCwiczenDoDodania;
        }
        planService.EdytujPlan(planDoEdycji, nowaNazwa, nowyPoziom.Value, nowyRodzaj, nowaIloscObwodow.Value, nowaPrzerwaMiedzyObwodami.Value, cwiczeniaDoDodania);
        renderer.PokazSukces("Udało się pomyślnie edytować plan!");
    }
}