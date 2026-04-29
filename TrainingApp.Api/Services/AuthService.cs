using TrainingApp.Api.Data;
using TrainingApp.Api.Dtos;
using TrainingApp.Api.Models;

namespace TrainingApp.Api.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    public AuthService(AppDbContext context)
    {
        _context = context;
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
}