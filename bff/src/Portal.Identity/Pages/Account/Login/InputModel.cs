using System.ComponentModel.DataAnnotations;

namespace Portal.Identity.Pages.Account.Login;

public class InputModel
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; init; }

    public bool RememberLogin { get; init; }

    public string? ReturnUrl { get; init; }

    public string? Button { get; set; }
}