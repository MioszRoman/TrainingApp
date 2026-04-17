using Microsoft.EntityFrameworkCore;
using TrainingApp.Api.Models;
using TrainingApp.Api.Data;

namespace TrainingApp.Api.Services;
public class PlanService
{
    private readonly AppDbContext _context;
    public PlanService(AppDbContext context)
    {
        _context = context;
    }

    public List<Plan> GetAllPlans()
    {
        var plany = _context.Plany
        .Include(plany => plany.Cwiczenia)
        .ToList();

        return plany;
    }
    public Plan? GetPlanById(int id)
    {
        var plan = _context.Plany
        .Include(p => p.Cwiczenia)
        .FirstOrDefault(p => p.Id == id);
        return plan;
    }
    public Plan CreatePlan(Plan plan)
    {
        _context.Plany.Add(plan);
        _context.SaveChanges();
        return plan;
    }
    public int DeletePlanById(int id)
    {
        var plan = _context.Plany.Find(id);
        if(plan == null)
        {
            return 0;
        }
        else if(_context.HistoriaTreningow.Any(h => h.PlanId == id))
        {
            return -1;
        }
        _context.Plany.Remove(plan);
        _context.SaveChanges();
        return 1;
    }
    public int UpdatePlan(int id, Plan updatedPlan)
    {
        var plan = _context.Plany
        .Include(plan => plan.Cwiczenia)
        .FirstOrDefault(plan => plan.Id == id);
        if(plan == null)
        {
            return 0;
        }
        plan.Nazwa = updatedPlan.Nazwa;
        plan.Poziom = updatedPlan.Poziom;
        plan.Rodzaj = updatedPlan.Rodzaj;
        plan.IloscObwodow = updatedPlan.IloscObwodow;
        plan.PrzerwaMiedzyObwodami = updatedPlan.PrzerwaMiedzyObwodami;
        plan.Cwiczenia = updatedPlan.Cwiczenia;
        _context.SaveChanges();
        return 1;
    }
}

