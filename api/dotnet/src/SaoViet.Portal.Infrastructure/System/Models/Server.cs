namespace SaoViet.Portal.Infrastructure.System.Models;

public sealed class Server
{
    public string? Name { get; set; }
    public DateTime Time { get; set; }
    public TimeSpan UpTime { get; set; }
    public object? CpuUsage { get; set; }
    public object? MemoryUsage { get; set; }
    public object? DiskUsage { get; set; }
}