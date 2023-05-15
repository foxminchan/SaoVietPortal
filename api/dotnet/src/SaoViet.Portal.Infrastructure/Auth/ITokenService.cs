using System.Security.Claims;
using SaoViet.Portal.Domain.Entities;

namespace SaoViet.Portal.Infrastructure.Auth;

public interface ITokenService
{
    public string GenerateToken(ApplicationUser user);

    public string GenerateRefreshToken();

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}