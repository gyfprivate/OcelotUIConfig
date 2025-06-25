using System.Security.Claims;

namespace OcelotManagement.Contracts.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role);
        ClaimsPrincipal ValidateToken(string token);
    }
}
