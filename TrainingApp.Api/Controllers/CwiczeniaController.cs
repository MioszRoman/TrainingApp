using Microsoft.AspNetCore.Mvc;
using TrainingApp.Api.Dtos;
using TrainingApp.Api.Services;

namespace TrainingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CwiczeniaController : ControllerBase
{
    private readonly PlanService _planService;
    public CwiczeniaController(PlanService planService)
    {
        _planService = planService;
    }
    
    [HttpPut("{id}")]
    public ActionResult UpdateCwiczenie(int id, [FromBody] UpdateCwiczenieDto dto)
    {
        var result = _planService.UpdateCwiczenie(id, dto);
        if(result == 0) return NotFound();
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCwiczenie(int id)
    {
        var result = _planService.DeleteCwiczenie(id);
        if(result == 0) return NotFound();
        return Ok();
    }
}