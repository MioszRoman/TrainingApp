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

    public List<PlanDto> GetAllPlans()
    {
        var plany = _context.Plany
        .Include(plany => plany.Cwiczenia)
        .ToList();

        return plany.Select(p => new PlanDto
        {
            Id = p.Id,
            Nazwa = p.Nazwa,
            Poziom = p.Poziom,
            Rodzaj = p.Rodzaj,
            IloscObwodow = p.IloscObwodow,
            PrzerwaMiedzyObwodami = p.PrzerwaMiedzyObwodami,
            Cwiczenia = p.Cwiczenia.Select(c => new CwiczenieDto
            {
                Id = c.Id,
                NazwaCwiczenia = c.NazwaCwiczenia,
                LiczbaSerii = c.LiczbaSerii,
                LiczbaPowtorzen = c.LiczbaPowtorzen,
                PrzerwaMiedzySeriami = c.PrzerwaMiedzySeriami
            }).ToList()
        }).ToList();
    }
    public PlanDto? GetPlanById(int id)
    {
        var plan = _context.Plany
        .Include(p => p.Cwiczenia)
        .FirstOrDefault(p => p.Id == id);
        if(plan == null)
        {
            return null;
        }
        return new PlanDto
        {
            Id = plan.Id,
            Nazwa = plan.Nazwa,
            Poziom = plan.Poziom,
            Rodzaj = plan.Rodzaj,
            IloscObwodow = plan.IloscObwodow,
            PrzerwaMiedzyObwodami = plan.PrzerwaMiedzyObwodami,
            Cwiczenia = plan.Cwiczenia.Select(c => new CwiczenieDto
            {
                Id = c.Id,
                NazwaCwiczenia = c.NazwaCwiczenia,
                LiczbaSerii = c.LiczbaSerii,
                LiczbaPowtorzen = c.LiczbaPowtorzen,
                PrzerwaMiedzySeriami = c.PrzerwaMiedzySeriami
            }).ToList()
        };
    }
    public List<CwiczenieDto> GetCwiczeniaByPlanId(int planId)
    {
        var plan = _context.Plany
        .Include(p => p.Cwiczenia)
        .FirstOrDefault(p => p.Id == planId);
        if(plan == null)
        {
            return new List<CwiczenieDto>();
        }
        return plan.Cwiczenia.Select(c => new CwiczenieDto
            {
                Id = c.Id,
                NazwaCwiczenia = c.NazwaCwiczenia,
                LiczbaSerii = c.LiczbaSerii,
                LiczbaPowtorzen = c.LiczbaPowtorzen,
                PrzerwaMiedzySeriami = c.PrzerwaMiedzySeriami
            }).ToList();
    }
    public CwiczenieDto? AddCwiczenieToPlan(int planId, CreateCwiczenieDto dto)
    {
        var plan = _context.Plany.FirstOrDefault(p => p.Id == planId);
        if(plan == null)
        {
            return null;
        }
        var cwiczenie = new Cwiczenie(dto.NazwaCwiczenia, dto.LiczbaSerii, dto.LiczbaPowtorzen, dto.PrzerwaMiedzySeriami);
        cwiczenie.PlanId = planId;

        _context.Cwiczenia.Add(cwiczenie);
        _context.SaveChanges();
        return new CwiczenieDto
        {
            Id = cwiczenie.Id,
            NazwaCwiczenia = cwiczenie.NazwaCwiczenia,
            LiczbaSerii = cwiczenie.LiczbaSerii,
            LiczbaPowtorzen = cwiczenie.LiczbaPowtorzen,
            PrzerwaMiedzySeriami = cwiczenie.PrzerwaMiedzySeriami
        };
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

