namespace Portal.Identity.Pages.Account.Logout;

public class LoggedOutViewModel
{
    public string? PostLogoutRedirectUri { get; init; }
    public string? ClientName { get; init; }
    public string? SignOutIframeUrl { get; init; }
    public bool AutomaticRedirectAfterSignOut { get; init; }
}