using System;
using System.Collections.Generic;
using System.Linq;
using TreningApp.Models;

namespace TreningApp;

class PlanService
{
    
    private AppDbContext _context;
    public PlanService(AppDbContext context) 
    {
        _context = context;
    }
    public List<Plan> GetPlany()
    {
        return _context.Plany.ToList();
    }
    public int DodajPlan(string nazwaPlanu, int poziom, string rodzajTreningu, int iloscObwodow, int przerwaO, List<Cwiczenie> listaCwiczen)
    {
        Plan planDoDodania = new Plan(nazwaPlanu, poziom, rodzajTreningu, iloscObwodow, przerwaO, listaCwiczen);
        _context.Plany.Add(planDoDodania);
        _context.SaveChanges();
        return planDoDodania.Id;
    }
    public Plan? ZnajdzPlanPoId(int id)
    {
        return _context.Plany.FirstOrDefault(plan => plan.Id == id);
        
    }
    public bool UsunPlanPoId(int id)
    {
        Plan? planDoUsuniecia = ZnajdzPlanPoId(id);
        if(planDoUsuniecia == null)
        {
            return false;
        }
        _context.Plany.Remove(planDoUsuniecia);
        _context.SaveChanges();
        return true;
    }
    public void EdytujPlan(Plan planDoEdycji, string nazwa, int poziom, string rodzaj, int iloscObwodow, int przerwaO, List<Cwiczenie> noweCwiczenia)
    {
        planDoEdycji.Nazwa = nazwa;
        planDoEdycji.Poziom = poziom;
        planDoEdycji.Rodzaj = rodzaj;
        planDoEdycji.IloscObwodow = iloscObwodow;
        planDoEdycji.PrzerwaMiedzyObwodami = przerwaO;
        planDoEdycji.Cwiczenia = noweCwiczenia;
        _context.SaveChanges();
    }
}