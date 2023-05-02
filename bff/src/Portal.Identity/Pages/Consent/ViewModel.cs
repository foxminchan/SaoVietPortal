namespace Portal.Identity.Pages.Consent;

public class ViewModel
{
    public string? ClientName { get; init; }
    public string? ClientUrl { get; init; }
    public string? ClientLogoUrl { get; init; }
    public bool AllowRememberConsent { get; init; }

    public IEnumerable<ScopeViewModel>? IdentityScopes { get; set; }
    public IEnumerable<ScopeViewModel>? ApiScopes { get; set; }
}

public class ScopeViewModel
{
    public string? Name { get; set; }
    public string? Value { get; set; }
    public string? DisplayName { get; set; }
    public string? Description { get; set; }
    public bool Emphasize { get; set; }
    public bool Required { get; set; }
    public bool Checked { get; set; }
    public IEnumerable<ResourceViewModel>? Resources { get; set; }
}

public class ResourceViewModel
{
    public string? Name { get; set; }
    public string? DisplayName { get; init; }
}