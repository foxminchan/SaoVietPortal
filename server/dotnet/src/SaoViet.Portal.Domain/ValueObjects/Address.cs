using System.Text.RegularExpressions;
using SaoViet.Portal.Domain.Primitives;

namespace SaoViet.Portal.Domain.ValueObjects;

public partial class Address : ValueObject
{
    public string? Street { get; init; }
    public string? Ward { get; init; }
    public string? District { get; init; }
    public string? City { get; init; }
    public string? ZipCode { get; init; }

    public Address()
    { }

    public Address(string street, string ward, string district, string city, string zipCode)
        => (Street, Ward, District, City, ZipCode) = (street, ward, district, city, zipCode);

    [GeneratedRegex("^\\d{5}(?:[-\\s]\\d{4})?$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex ZipCodePattern();

    private static readonly Lazy<Regex> ZipCodeRegex = new(ZipCodePattern);

    public static bool IsValidZipCode(string? zipCode) =>
        !string.IsNullOrWhiteSpace(zipCode) && ZipCodeRegex.Value.IsMatch(zipCode);

    public override string ToString() => $"{Street}, {Ward}, {District}, {City}, {ZipCode}";

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return ToString();
    }
}