using System.Security.Claims;
using Portal.Domain.Entities;

namespace Portal.Application.Token;

public interface ITokenService
{
    public string GenerateToken(ApplicationUser user);
    public string GenerateRefreshToken();
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}