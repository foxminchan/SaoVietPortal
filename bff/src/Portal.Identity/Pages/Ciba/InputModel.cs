namespace Portal.Identity.Pages.Ciba;

public class InputModel
{
    public string? Button { get; set; }
    public IEnumerable<string>? ScopesConsented { get; set; }
    public string? Id { get; init; }
    public string? Description { get; init; }
}