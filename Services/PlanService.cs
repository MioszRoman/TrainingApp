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
    private int nextId = 1;
    public List<Plan> GetPlany()
    {
        return plany;
    }
    static JsonSerializerOptions JsonOptions()
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
    }
    public int DodajPlan(string nazwaPlanu, int poziom, string rodzajTreningu, int iloscObwodow, int przerwaO, List<Cwiczenie> listaCwiczen)
    {
        int noweId = nextId;
        plany.Add(new Plan(nazwaPlanu, poziom, rodzajTreningu, iloscObwodow,noweId , przerwaO, listaCwiczen));
        nextId++;
        ZapisPlanow();
        return noweId;
    }
    public Plan? ZnajdzPlanPoId(int id)
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
    public bool UsunPlanPoId(int id)
    {
        Plan? planDoUsuniecia = ZnajdzPlanPoId(id);
        if(planDoUsuniecia == null)
        {
            return false;
        }
        plany.Remove(planDoUsuniecia);
        ZapisPlanow();
        return true;
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