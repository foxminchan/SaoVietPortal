namespace Portal.Identity.Pages.Device;

public class InputModel
{
    public string? Button { get; set; }
    public IEnumerable<string>? ScopesConsented { get; set; }
    public bool RememberConsent { get; init; } = true;
    public string? ReturnUrl { get; set; }
    public string? Description { get; init; }
    public string? UserCode { get; init; }
}