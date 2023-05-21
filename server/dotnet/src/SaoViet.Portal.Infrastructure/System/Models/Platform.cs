namespace SaoViet.Portal.Infrastructure.System.Models;

public sealed class Platform
{
    public string? AppName { get; set; }
    public string? EnvironmentName { get; set; }
    public string? OsArchitecture { get; set; }
    public string? OsDescription { get; set; }
    public string? ProcessArchitecture { get; set; }
    public string? BasePath { get; set; }
    public string? FrameworkDescription { get; set; }
    public string? HostName { get; set; }
    public string? IpAddress { get; set; }
    public Dictionary<string, string?> EnvironmentVariables { get; set; } = new();
}