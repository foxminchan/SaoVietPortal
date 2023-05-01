namespace Portal.Domain.Options;

public sealed class SerilogOptions
{
    public bool UseConsole { get; private set; } = true;
    public Uri SeqUrl { get; private set; } = new("http://localhost:5341");
    public Uri ElasticSearchUrl { get; private set; } = new("http://localhost:9200");

    public string LogTemplate { get; private set; } =
        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level} - {Message:lj}{NewLine}{Exception}";
}