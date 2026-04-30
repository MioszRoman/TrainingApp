using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Principal;
using TrainingApp.Api.Data;
using TrainingApp.Api.Dtos;
using TrainingApp.Api.Models;

namespace TrainingApp.Api.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    public bool Register(RegisterDto dto)
    {
        bool userExists = _context.Users.Any(u => u.Username == dto.Username);
        if(userExists)
        {
            return false;
        }
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        User user = new User
        {
            Username = dto.Username,
            PasswordHash = passwordHash
        };
        _context.Users.Add(user);
        _context.SaveChanges();
        return true;
    }

    public AuthResponseDto? Login(LoginDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == dto.Username);
        if(user == null) return null;
        bool passwordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        if(!passwordValid) return null;
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.Now.AddMinutes(
            double.Parse(_configuration["Jwt:ExpiresInMinutes"]!)
        );
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return new AuthResponseDto
        {
            Token = jwt
        };
    }
}