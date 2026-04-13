using System;
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
    static StatystykiService statystykiService = new StatystykiService();
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
                    renderer.PokazNaglowek("Twoje treningi");
                    renderer.Separator();
                    foreach(var plan in planService.GetPlany())
                    {
                        renderer.WyswietlPlan(plan);
                    }
                    break;
                case "3":
                    RozpocznijTrening();
                    break;
                case "4":
                    renderer.PokazNaglowek("Twoje plany z cwiczeniami: ");
                    renderer.MocnySeparator();
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
                    var wpisy = historiaService.PobierzWszystkieWpisy();
                    renderer.WyswietlHistorie(wpisy);
                    break;
                case "8":
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
                    break;
                case "9":
                    FiltrujHistoriePoDacie();
                    break;
                case "10":
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
                    break;
                case "11":
                    var wpisyDoStatystyk = historiaService.PobierzWszystkieWpisy();
                    statystykiService.WyswietlStatystyki(wpisyDoStatystyk);
                    break;
                case "12":
                    renderer.PokazKomunikat("Zamykam aplikację...");
                    isRunning = false;
                    break;
                default:
                    if(choice == null)
                    {
                    break;
                    }
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
        renderer.PokazKomunikat("7. Wyswietl historie treningow");
        renderer.PokazKomunikat("8. Wyświetl plan po id");
        renderer.PokazKomunikat("9. Wyświetl historie z konkretnego przedziału.");
        renderer.PokazKomunikat("10. Usuń wpis z historii");
        renderer.PokazKomunikat("11. Statystyki");
        renderer.PokazKomunikat("12. Zamknij aplikację");
        renderer.MocnySeparator();
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
            renderer.PokazBlad("Nie udało się pobrać przerwy miedzy obwodami!\nUpewnij się że podałeś odpowieną długość przerwy!");
            return;
        }
        List<Cwiczenie> listaCwiczen = inputHelper.PobierzCwiczenia();
        if(listaCwiczen == null)
        {
            renderer.PokazBlad("Nie udało się pobrać listy cwiczen!\nTworzenie planu zostało przerwane.");
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
            renderer.PokazBlad("Nie znaleziono treningu o podanym id. Upewnij się, że podałeś poprawne ID.");
        }
    }
    static void FiltrujHistoriePoDacie()
    {
        renderer.PokazKomunikatBezNowejLinii("Podaj datę OD (dd.MM.yyyy): ");
        string odInput = Console.ReadLine();
        if(!DateTime.TryParse(odInput, out DateTime od))
        {
            renderer.PokazBlad("Nie podano poprawnej daty!");
            return;
        }
        renderer.PokazKomunikatBezNowejLinii("Podaj datę Do (dd.MM.yyyy): ");
        string doInput = Console.ReadLine();
        if(!DateTime.TryParse(doInput, out DateTime doKiedy))
        {
            renderer.PokazBlad("Nie podano poprawnej daty!");
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
            if(idDoEdycji == -1)
            {   
                return;
            }
            Plan planDoEdycji = planService.ZnajdzPlanPoId(idDoEdycji);
            if(planDoEdycji == null)
            {
                renderer.PokazBlad("Nie ma takiego planu!");
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