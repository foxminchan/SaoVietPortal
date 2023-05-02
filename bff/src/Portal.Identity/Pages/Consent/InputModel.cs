namespace Portal.Identity.Pages.Consent;

public class InputModel
{
    public string? Button { get; set; }
    public IEnumerable<string>? ScopesConsented { get; set; }
    public bool RememberConsent { get; init; } = true;
    public string? ReturnUrl { get; init; }
    public string? Description { get; init; }
}