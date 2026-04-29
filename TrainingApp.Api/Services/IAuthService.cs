using TrainingApp.Api.Dtos;

namespace TrainingApp.Api.Services;

public interface IAuthService
{
    bool Register(RegisterDto dto);
}