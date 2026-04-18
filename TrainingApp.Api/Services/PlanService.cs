using Microsoft.EntityFrameworkCore;
using TrainingApp.Api.Models;
using TrainingApp.Api.Data;
using TrainingApp.Api.Dtos;

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
    public Plan CreatePlan(CreatePlanDto dto)
    {
        Plan createdPlan = new Plan(dto.Nazwa, dto.Poziom, dto.Rodzaj, dto.IloscObwodow, dto.PrzerwaMiedzyObwodami, new List<Cwiczenie>());
        _context.Plany.Add(createdPlan);
        _context.SaveChanges();
        return createdPlan;
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
    public int UpdatePlan(int id, UpdatePlanDto dto)
    {
        var plan = _context.Plany
        .Include(plan => plan.Cwiczenia)
        .FirstOrDefault(plan => plan.Id == id);
        if(plan == null)
        {
            return 0;
        }
        plan.Nazwa = dto.Nazwa;
        plan.Poziom = dto.Poziom;
        plan.Rodzaj = dto.Rodzaj;
        plan.IloscObwodow = dto.IloscObwodow;
        plan.PrzerwaMiedzyObwodami = dto.PrzerwaMiedzyObwodami;
        //plan.Cwiczenia = dto.Cwiczenia;
        _context.SaveChanges();
        return 1;
    }
}

