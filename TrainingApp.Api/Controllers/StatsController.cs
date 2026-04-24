using Microsoft.AspNetCore.Mvc;
using TrainingApp.Api.Services;
using TrainingApp.Api.Dtos;

namespace TrainingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatsController : ControllerBase
{
    private readonly IStatsService _statsService;
    public StatsController(IStatsService statsService)
    {
        _statsService = statsService;
    }
    [HttpGet]
    public ActionResult<StatsDto> GetStats()
    {
        var stats = _statsService.GetStats();
        if(stats == null) return NotFound();
        return Ok(stats);
    }
}