namespace Portal.Api.Models;

public record Position(int? Id, string Name)
{
    public Position() : this(null, string.Empty) { }
}