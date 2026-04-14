using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using TreningApp.Models;
namespace TreningApp;

class PlanService
{
    private List<Plan> plany = new List<Plan>();
    private string sciezkaPlanow = Path.Combine("Data", "Plany.json");
    private InputHelper inputHelper = new InputHelper();
    private int nextId = 1;
    public List<Plan> GetPlany()
    {
        return plany;
    }
    /*public int GetNextId()
    {
        return nextId;
    }*/
    private JsonSerializerOptions JsonOptions()
    {
        var options = new JsonSerializerOptions();
        options.TypeInfoResolver = new DefaultJsonTypeInfoResolver();
        options.WriteIndented = true;
        return options;
    }
    public void ZapisPlanow()
    {
        string jsonString = JsonSerializer.Serialize(plany, JsonOptions());
        File.WriteAllText(sciezkaPlanow, jsonString);
        //Console.WriteLine("Zapisano pomyślnie do pliku Plany.json");
    }
    public void OdczytPlanow()
    {
        if(!File.Exists(sciezkaPlanow))
        {
            return;
        }
        string zapis = File.ReadAllText(sciezkaPlanow);
        if(string.IsNullOrWhiteSpace(zapis))
        {
            plany = new List<Plan>();
            nextId = 1;
            return;
        }
        var des = JsonSerializer.Deserialize<List<Plan>>(zapis, JsonOptions());
        if(des != null)
        {
            plany = des;
        }
        int maxId = 0; //Zmienna która pomoże określić maksymalną wartość id znajdującego się w plany.json
        foreach(var plan in plany)
        {
            if(plan.Id > maxId)
            {
                maxId = plan.Id;
            }
        }
        if(plany.Count > 0)
        {
            nextId = maxId + 1;
        }
        else
        {
            nextId = 1;
        }
        //Druga opcja "bardziej pro" ----> nextId = plany.Max(p => p.Id) + 1; 
        //Potrzebne do tego jeszcze using System.Linq; 
    }
    public void DodajPlan(string nazwaPlanu, int poziom, string rodzajTreningu, int iloscObwodow, int przerwaO, List<Cwiczenie> listaCwiczen)
    {
        int noweId = nextId;
        plany.Add(new Plan(nazwaPlanu, poziom, rodzajTreningu, iloscObwodow,noweId , przerwaO, listaCwiczen));
        nextId++;
        ZapisPlanow();
        Console.WriteLine("Trening został dodany pomyślnie, jego ID to: " + noweId);
    }
    public Plan ZnajdzPlanPoId(int id)
    {
        foreach(var plan in plany)
        {
            if(plan.Id == id)
            {
                return plan;
            }
        }
        return null;
    }
    public void UsunPlanPoId(int id)
    {
        Plan planDoUsuniecia = ZnajdzPlanPoId(id);
        if(planDoUsuniecia == null)
        {
            Console.WriteLine("Nie ma planu o takim ID!");
            return;
        }
        plany.Remove(planDoUsuniecia);
        Console.WriteLine("Pomyślnie usunięto plan!");
        ZapisPlanow();
    }
    public void EdytujPlanPoId(Plan planDoEdycji, string nazwa, int poziom, string rodzaj, int iloscObwodow, int przerwaO, List<Cwiczenie> noweCwiczenia)
    {
        planDoEdycji.Nazwa = nazwa;
        planDoEdycji.Poziom = poziom;
        planDoEdycji.Rodzaj = rodzaj;
        planDoEdycji.IloscObwodow = iloscObwodow;
        planDoEdycji.PrzerwaMiedzyObwodami = przerwaO;
        planDoEdycji.Cwiczenia = noweCwiczenia;
        ZapisPlanow();

    }
}