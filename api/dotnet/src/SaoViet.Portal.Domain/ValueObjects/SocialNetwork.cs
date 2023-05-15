using SaoViet.Portal.Domain.Primitives;

namespace SaoViet.Portal.Domain.ValueObjects;

public class SocialNetwork : ValueObject
{
    private string? Name { get; init; }
    private string? Url { get; init; }

    public SocialNetwork()
    { }

    public SocialNetwork(string name, string url)
        => (Name, Url) = (name, url);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
        yield return Url;
    }
}