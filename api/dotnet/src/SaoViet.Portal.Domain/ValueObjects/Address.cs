using SaoViet.Portal.Domain.Primitives;

namespace SaoViet.Portal.Domain.ValueObjects;

public class Address : ValueObject
{
    private string? Street { get; init; }
    private string? Ward { get; init; }
    private string? District { get; init; }
    private string? City { get; init; }

    public Address()
    { }

    public Address(string street, string ward, string district, string city)
        => (Street, Ward, District, City) = (street, ward, district, city);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return $"{Street}, {Ward}, {District}, {City}";
    }
}