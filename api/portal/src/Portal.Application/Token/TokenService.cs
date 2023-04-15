using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Application.Token;

public class TokenService : ITokenService
{
    private readonly string _issuer;
    private readonly SigningCredentials _jwtSigningCredentials;
    private readonly Claim[] _audiences;
    private readonly IUnitOfWork _unitOfWork;

    public TokenService(IAuthenticationConfigurationProvider authenticationConfigurationProvider, IUnitOfWork unitOfWork)
    {
        var bearerSection = authenticationConfigurationProvider.GetSchemeConfiguration(JwtBearerDefaults.AuthenticationScheme);

        var section = bearerSection.GetSection("SigningKeys:0");

        _issuer = bearerSection["ValidIssuer"] ?? throw new InvalidOperationException("Issuer is not specified");
        var signingKeyBase64 = section["Value"] ?? throw new InvalidOperationException("Signing key is not specified");

        var signingKeyBytes = Convert.FromBase64String(signingKeyBase64);

        _jwtSigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKeyBytes),
            SecurityAlgorithms.HmacSha512Signature);

        _audiences = bearerSection.GetSection("ValidAudiences").GetChildren()
            .Where(s => !string.IsNullOrEmpty(s.Value))
            .Select(s => new Claim(JwtRegisteredClaimNames.Aud, s.Value!))
            .ToArray();

        _unitOfWork = unitOfWork;
    }

    public string GenerateToken(ApplicationUser user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Iss, _issuer));
        identity.AddClaims(_audiences);

        var roles = _unitOfWork.userRepository.GetRoles(user.Id)
            .Where(role => !string.IsNullOrEmpty(role))
            .Select(role => new Claim(ClaimTypes.Role, role ?? string.Empty));

        var claims = _unitOfWork.userRepository.GetClaims(user.Id)
            .Where(claim => !string.IsNullOrEmpty(claim))
            .Select(claim => new Claim("Permission", claim ?? string.Empty));

        var roleClaims = _unitOfWork.userRepository.GetRoleClaims(user.Id)
            .Where(roleClaim => !string.IsNullOrEmpty(roleClaim))
            .Select(roleClaim => new Claim("Permission", roleClaim ?? string.Empty));

        identity.AddClaims(roles);
        identity.AddClaims(claims);
        identity.AddClaims(roleClaims);

        var token = new JwtSecurityToken(
                       issuer: _issuer,
                       claims: identity.Claims,
                       notBefore: DateTime.UtcNow,
                       expires: DateTime.UtcNow.AddDays(1),
                       signingCredentials: _jwtSigningCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _issuer,
            ValidateAudience = true,
            ValidAudiences = _audiences.Select(c => c.Value).ToArray(),
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _jwtSigningCredentials.Key,
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
        return principal;
    }
}