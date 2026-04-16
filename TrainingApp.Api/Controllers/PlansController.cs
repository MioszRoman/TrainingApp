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
}