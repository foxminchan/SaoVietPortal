using SaoViet.Portal.Domain.Primitives;
using System.Text.RegularExpressions;

namespace SaoViet.Portal.Domain.ValueObjects;

public partial class SocialNetwork : ValueObject
{
    public string? Name { get; init; }
    public string? Url { get; init; }

    public SocialNetwork()
    { }

    public SocialNetwork(string name, string url)
        => (Name, Url) = (name, url);

    [GeneratedRegex(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex UrlPattern();

    private static readonly Lazy<Regex> UrlRegex = new(UrlPattern);

    public static bool IsValidUrl(string? url) =>
        !string.IsNullOrWhiteSpace(url) && UrlRegex.Value.IsMatch(url);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
        yield return Url;
    }
}