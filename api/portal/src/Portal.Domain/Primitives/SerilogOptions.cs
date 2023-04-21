namespace Portal.Domain.Primitives;

public sealed class SerilogOptions
{
    public bool UseConsole { get; set; } = true;
    public string? SeqUrl { get; set; } = "http://localhost:5341";
    public string? ElasticSearchUrl { get; set; } = "http://localhost:9200";
    public string LogTemplate { get; set; } =
        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level} - {Message:lj}{NewLine}{Exception}";
}