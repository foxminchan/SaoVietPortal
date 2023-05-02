namespace Portal.Identity.Pages.Grants;

public class ViewModel
{
    public IEnumerable<GrantViewModel>? Grants { get; init; }
}

public class GrantViewModel
{
    public string? ClientId { get; init; }
    public string? ClientName { get; init; }
    public string? ClientUrl { get; set; }
    public string? ClientLogoUrl { get; init; }
    public string? Description { get; init; }
    public DateTime Created { get; init; }
    public DateTime? Expires { get; init; }
    public IEnumerable<string>? IdentityGrantNames { get; init; }
    public IEnumerable<string>? ApiGrantNames { get; init; }
}