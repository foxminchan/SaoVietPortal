using Portal.Domain.Entities;
using System.Security.Claims;

namespace Portal.Application.Token;

public interface ITokenService
{
    public string GenerateToken(ApplicationUser user);

    public string GenerateRefreshToken();

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}