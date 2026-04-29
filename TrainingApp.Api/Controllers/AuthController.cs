using Microsoft.AspNetCore.Mvc;
using TrainingApp.Api.Dtos;
using TrainingApp.Api.Services;

namespace TrainingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public ActionResult Register([FromBody] RegisterDto dto)
    {
        bool result = _authService.Register(dto);
        if(!result)
        {
            return BadRequest("Użytkownik o takiej nazwie już istnieje.");
        }
        return Ok("Użytkownik został zarejestrowany.");
    }

    [HttpPost("login")]
    public ActionResult<AuthResponseDto> Login([FromBody] LoginDto dto)
    {
        var result = _authService.Login(dto);
        if(result == null)
        {
            return Unauthorized("Nieprawidłowy login lub hasło.");
        }
        return Ok(result);
    }
}