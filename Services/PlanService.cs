using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
        return _context.Plany.Include(plan => plan.Cwiczenia).ToList();
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
        return _context.Plany.Include(plan => plan.Cwiczenia).FirstOrDefault(plan => plan.Id == id);
        
    }
    public int UsunPlanPoId(int id)
    {
        Plan? planDoUsuniecia = ZnajdzPlanPoId(id);

        if(planDoUsuniecia == null)
        {
            return 0;
        }
        else if(_context.HistoriaTreningow.Any(h => h.PlanId == id))
        {
            return -1;
        }
        _context.Plany.Remove(planDoUsuniecia);
        _context.SaveChanges();
        return 1;
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