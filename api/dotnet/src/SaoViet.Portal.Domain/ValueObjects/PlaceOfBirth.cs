using SaoViet.Portal.Domain.Primitives;

namespace SaoViet.Portal.Domain.ValueObjects;

public class PlaceOfBirth : ValueObject
{
    private string? City { get; init; }
    private string? Province { get; init; }

    public PlaceOfBirth()
    { }

    public PlaceOfBirth(string? city, string? province)
        => (City, Province) = (city, province);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return $"{City}, {Province}";
    }
}