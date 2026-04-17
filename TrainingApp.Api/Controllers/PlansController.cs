using Microsoft.AspNetCore.Mvc;
using TrainingApp.Api.Models;
using TrainingApp.Api.Services;

namespace TrainingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlansController : ControllerBase
{
    private readonly PlanService _planService;
    public PlansController(PlanService planService)
    {
        _planService = planService;
    }

    [HttpGet]
    public ActionResult<List<Plan>> GetAll()
    {
        return Ok(_planService.GetAllPlans());
    }
    

    [HttpGet("{id}")]
    public ActionResult<Plan> GetById(int id)
    {
        var plan = _planService.GetPlanById(id);
        if(plan == null)
        {
            return NotFound();
        }
        return Ok(plan);
    }

    [HttpPost]
    public ActionResult<Plan> CreatePlan([FromBody] Plan plan)
    {
        _planService.CreatePlan(plan);
        return CreatedAtAction(nameof(GetById), new {id = plan.Id}, plan);
    }

    [HttpDelete("{id}")]
    public ActionResult DeletePlan(int id)
    {
        var plan = _planService.DeletePlanById(id);
        if(plan == 0)
        {
            return NotFound();
        }
        else if(plan == -1)
        {
            return BadRequest("Plan ma wpis w historii i nie może być usunięty");
        }
        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult<Plan> UpdatePlan(int id, [FromBody] Plan updatedPlan)
    {
        var plan = _planService.UpdatePlan(id, updatedPlan);
        if(plan == 0)
        {
            return NotFound();
        }
        return Ok();
    }
}