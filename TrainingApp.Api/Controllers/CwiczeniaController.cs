using Microsoft.AspNetCore.Mvc;
using TrainingApp.Api.Dtos;
using TrainingApp.Api.Services;

namespace TrainingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CwiczeniaController : ControllerBase
{
    private readonly IPlanService _planService;
    public CwiczeniaController(IPlanService planService)
    {
        _planService = planService;
    }
    
    [HttpPut("{id}")]
    public ActionResult UpdateCwiczenie(int id, [FromBody] UpdateCwiczenieDto dto)
    {
        var result = _planService.UpdateCwiczenie(id, dto);
        if(result == 0) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCwiczenie(int id)
    {
        var result = _planService.DeleteCwiczenie(id);
        if(result == 0) return NotFound();
        return NoContent();
    }
}