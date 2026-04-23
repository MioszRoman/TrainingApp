using Microsoft.AspNetCore.Mvc;
using TrainingApp.Api.Models;
using TrainingApp.Api.Services;
using TrainingApp.Api.Dtos;

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
    public ActionResult<List<PlanDto>> GetAll()
    {
        return Ok(_planService.GetAllPlans());
    }
    

    [HttpGet("{id}")]
    public ActionResult<PlanDto> GetById(int id)
    {
        var plan = _planService.GetPlanById(id);
        if(plan == null)
        {
            return NotFound();
        }
        return Ok(plan);
    }

    [HttpPost]
    public ActionResult<Plan> CreatePlan([FromBody] CreatePlanDto dto)
    {
        var plan = _planService.CreatePlan(dto);
        return CreatedAtAction(nameof(GetById), new {id = plan.Id}, plan);
    }

    [HttpDelete("{id}")]
    public ActionResult DeletePlan(int id)
    {
        var result = _planService.DeletePlanById(id);
        if(result == 0)
        {
            return NotFound();
        }
        else if(result == -1)
        {
            return BadRequest("Plan ma wpis w historii i nie może być usunięty");
        }
        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult UpdatePlan(int id, [FromBody] UpdatePlanDto dto)
    {
        var result = _planService.UpdatePlan(id, dto);
        if(result == 0)
        {
            return NotFound();
        }
        return Ok();
    }
    [HttpGet("{id}/cwiczenia")]
    public ActionResult<List<CwiczenieDto>> GetCwiczenia(int id)
    {
        var cwiczenia = _planService.GetCwiczeniaByPlanId(id);
        return Ok(cwiczenia);
    }
    [HttpPost("{id}/cwiczenia")]
    public ActionResult<CwiczenieDto> AddCwiczenie(int id, [FromBody] CreateCwiczenieDto dto)
    {
        var cwiczenie = _planService.AddCwiczenieToPlan(id, dto);
        if(cwiczenie == null)
        {
            return NotFound();
        }
        return Ok(cwiczenie);
    }
}