using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingApp.Api.Models;
using TrainingApp.Api.Data;

namespace TrainingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlansController : ControllerBase
{
    private readonly AppDbContext _context;
    public PlansController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<List<Plan>> GetAll()
    {
        var plany = _context.Plany
        .Include(plany => plany.Cwiczenia)
        .ToList();

        return Ok(plany);
    }

    [HttpGet("{id}")]
    public ActionResult<Plan> GetById(int id)
    {
        var plan = _context.Plany
        .Include(p => p.Cwiczenia)
        .FirstOrDefault(p => p.Id == id);
        if(plan == null)
        {
            return NotFound();
        }
        return Ok(plan);
    }

    [HttpPost]
    public ActionResult<Plan> CreatePlan([FromBody] Plan plan)
    {
        _context.Plany.Add(plan);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new {id = plan.Id}, plan);
    }

    [HttpDelete("{id}")]
    public ActionResult<Plan> DeletePlan(int id)
    {
        var plan = _context.Plany.Find(id);
        if(plan == null)
        {
            return NotFound();
        }
        else if(_context.HistoriaTreningow.Any(h => h.PlanId == id))
        {
            return BadRequest("Plan ma wpis w historii i nie może być usunięty");
        }
        _context.Plany.Remove(plan);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult<Plan> UpdatePlan(int id, [FromBody] Plan updatedPlan)
    {
        var plan = _context.Plany
        .Include(plan => plan.Cwiczenia)
        .FirstOrDefault(plan => plan.Id == id);
        if(plan == null)
        {
            return NotFound();
        }
        plan.Nazwa = updatedPlan.Nazwa;
        plan.Poziom = updatedPlan.Poziom;
        plan.Rodzaj = updatedPlan.Rodzaj;
        plan.IloscObwodow = updatedPlan.IloscObwodow;
        plan.PrzerwaMiedzyObwodami = updatedPlan.PrzerwaMiedzyObwodami;
        plan.Cwiczenia = updatedPlan.Cwiczenia;
        _context.SaveChanges();
        return Ok(plan);
    }
}