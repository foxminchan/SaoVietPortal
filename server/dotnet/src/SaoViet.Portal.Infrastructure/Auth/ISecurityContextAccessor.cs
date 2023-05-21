namespace SaoViet.Portal.Infrastructure.Auth;

public interface ISecurityContextAccessor
{
    public string? UserId { get; }
    public string? JwtToken { get; }
    public string? RefreshToken { get; }
    public string? IpAddress { get; }
    public bool IsAuthenticated { get; }
    public string? Role { get; }
    public string? Permission { get; }
}