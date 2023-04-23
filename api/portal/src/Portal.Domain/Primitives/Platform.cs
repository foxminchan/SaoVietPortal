namespace Portal.Domain.Primitives;

public sealed class Platform
{
    public string AppName { get; set; } = string.Empty;
    public string EnvironmentName { get; set; } = string.Empty;
    public string OsArchitecture { get; set; } = string.Empty;
    public string OsDescription { get; set; } = string.Empty;
    public string ProcessArchitecture { get; set; } = string.Empty;
    public string BasePath { get; set; } = string.Empty;
    public string FrameworkDescription { get; set; } = string.Empty;
    public string HostName { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public Dictionary<string, string?> EnvironmentVariables { get; set; } = new();
}