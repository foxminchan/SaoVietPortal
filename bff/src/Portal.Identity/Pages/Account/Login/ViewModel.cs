namespace Portal.Identity.Pages.Account.Login;

public class ViewModel
{
    public bool AllowRememberLogin { get; init; } = true;
    public bool EnableLocalLogin { get; init; } = true;

    public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();

    public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders
        .Where(x => !string.IsNullOrWhiteSpace(x.DisplayName));

    public bool IsExternalLoginOnly => EnableLocalLogin == false && ExternalProviders.Count() == 1;
    public string? ExternalLoginScheme => IsExternalLoginOnly ? ExternalProviders.SingleOrDefault()?.AuthenticationScheme : null;

    public class ExternalProvider
    {
        public string? DisplayName { get; init; }
        public string? AuthenticationScheme { get; init; }
    }
}