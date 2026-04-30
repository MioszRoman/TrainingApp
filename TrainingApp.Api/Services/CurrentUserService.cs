namespace TrainingApp.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public int GetCurrentUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User
        .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?
        .Value;
        if(userId == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }
        return int.Parse(userId);
    }
}