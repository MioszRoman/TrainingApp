using Microsoft.EntityFrameworkCore;
using TrainingApp.Api.Models;
using TrainingApp.Api.Data;
using TrainingApp.Api.Dtos;

namespace TrainingApp.Api.Services;
public class PlanService : IPlanService
{
    private readonly AppDbContext _context;
    public PlanService(AppDbContext context)
    {
        _context = context;
    }

    public PagedResultDto<PlanDto> GetAllPlans(int? poziom, string? rodzaj, int page, int pageSize)
    {
        var query = _context.Plany
        .Include(plany => plany.Cwiczenia)
        .AsQueryable();

        query = query.Where(p => p.UserId == 1);
        if(poziom.HasValue)
        {
            query = query.Where(p => p.Poziom == poziom.Value);
        }
        if(!string.IsNullOrWhiteSpace(rodzaj))
        {
            string rodzajLower = rodzaj.ToLower();
            query = query.Where(p => p.Rodzaj != null && p.Rodzaj.ToLower() == rodzajLower);
        }
        if(page < 1) page = 1;
        if(pageSize < 1) pageSize = 10;
        if(pageSize > 50) pageSize = 50;
        var totalCount = query.Count();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        query = query
        .OrderBy(p => p.Id)
        .Skip((page - 1) * pageSize)
        .Take(pageSize);
        var plany = query.ToList();
        var items = plany.Select(p => new PlanDto
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

        return new PagedResultDto<PlanDto>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }
    public PlanDto? GetPlanById(int id)
    {
        var plan = _context.Plany
        .Include(p => p.Cwiczenia)
        .FirstOrDefault(p => p.Id == id && p.UserId == 1);
        
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
    public int UpdateCwiczenie(int id, UpdateCwiczenieDto dto)
    {
        var cwiczenie = _context.Cwiczenia.FirstOrDefault(c => c.Id == id);
        if(cwiczenie == null)
        {
            return 0;
        }
        cwiczenie.NazwaCwiczenia = dto.NazwaCwiczenia;
        cwiczenie.LiczbaSerii = dto.LiczbaSerii;
        cwiczenie.LiczbaPowtorzen = dto.LiczbaPowtorzen;
        cwiczenie.PrzerwaMiedzySeriami = dto.PrzerwaMiedzySeriami;
        _context.SaveChanges();
        return 1;
    }
    public int DeleteCwiczenie(int id)
    {
        var cwiczenie = _context.Cwiczenia.FirstOrDefault(c => c.Id == id);
        if(cwiczenie == null) return 0;
        _context.Cwiczenia.Remove(cwiczenie);
        _context.SaveChanges();
        return 1;
    }
    public Plan CreatePlan(CreatePlanDto dto)
    {
        Plan createdPlan = new Plan(dto.Nazwa, dto.Poziom, dto.Rodzaj, dto.IloscObwodow, dto.PrzerwaMiedzyObwodami, new List<Cwiczenie>());
        createdPlan.UserId = 1;
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

