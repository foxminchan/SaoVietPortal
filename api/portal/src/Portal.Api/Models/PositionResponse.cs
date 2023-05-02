namespace Portal.Api.Models;

public record PositionResponse(int? Id, string Name)
{
    public PositionResponse() : this(null, string.Empty) { }
}