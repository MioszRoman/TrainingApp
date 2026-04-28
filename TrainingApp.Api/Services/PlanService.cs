using Microsoft.EntityFrameworkCore;
using TrainingApp.Api.Models;
using TrainingApp.Api.Data;
using TrainingApp.Api.Dtos;

namespace TrainingApp.Api.Services;
public class PlanService : IPlanService
{
    private readonly AppDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    public PlanService(AppDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    private CwiczenieDto MapToCwiczenieDto(Cwiczenie cwiczenie)
    {
        return new CwiczenieDto
        {
            Id = cwiczenie.Id,
            NazwaCwiczenia = cwiczenie.NazwaCwiczenia,
            LiczbaSerii = cwiczenie.LiczbaSerii,
            LiczbaPowtorzen = cwiczenie.LiczbaPowtorzen,
            PrzerwaMiedzySeriami = cwiczenie.PrzerwaMiedzySeriami
        };
    }
    private PlanDto MapToPlanDto(Plan plan)
    {
        return new PlanDto
        {
            Id = plan.Id,
            Nazwa = plan.Nazwa,
            Poziom = plan.Poziom,
            Rodzaj = plan.Rodzaj,
            IloscObwodow = plan.IloscObwodow,
            PrzerwaMiedzyObwodami = plan.PrzerwaMiedzyObwodami,
            Cwiczenia = plan.Cwiczenia
            .Select(c => MapToCwiczenieDto(c))
            .ToList()
        };
    }

    public PagedResultDto<PlanDto> GetAllPlans(int? poziom, string? rodzaj, int page, int pageSize)
    {
        int userId = _currentUserService.GetCurrentUserId();
        var query = _context.Plany
        .Include(plany => plany.Cwiczenia)
        .AsQueryable();

        query = query.Where(p => p.UserId == userId);
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
        var items = plany.
        Select(p => MapToPlanDto(p))
        .ToList();

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
        int userId = _currentUserService.GetCurrentUserId();
        var plan = _context.Plany
        .Include(p => p.Cwiczenia)
        .FirstOrDefault(p => p.Id == id && p.UserId == userId);
        
        if(plan == null)
        {
            return null;
        }
        return MapToPlanDto(plan);
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
        return plan.Cwiczenia
        .Select(c => MapToCwiczenieDto(c))
        .ToList();
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
        return MapToCwiczenieDto(cwiczenie);
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
        int userId = _currentUserService.GetCurrentUserId();
        Plan createdPlan = new Plan(dto.Nazwa, dto.Poziom, dto.Rodzaj, dto.IloscObwodow, dto.PrzerwaMiedzyObwodami, new List<Cwiczenie>());
        createdPlan.UserId = userId;
        _context.Plany.Add(createdPlan);
        _context.SaveChanges();
        return createdPlan;
    }
    public int DeletePlanById(int id)
    {
        int userId = _currentUserService.GetCurrentUserId();
        var plan = _context.Plany.FirstOrDefault(p => p.Id == id & p.UserId == userId);
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
        int userId = _currentUserService.GetCurrentUserId();
        var plan = _context.Plany
        .Include(plan => plan.Cwiczenia)
        .FirstOrDefault(plan => plan.Id == id && plan.UserId == userId);
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

