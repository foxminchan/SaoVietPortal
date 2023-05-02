using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using System.Text.Json;

namespace Portal.Identity.Pages.Diagnostics;

public class ViewModel
{
    public ViewModel(AuthenticateResult result)
    {
        AuthenticateResult = result;

        if (result.Properties is null || !result.Properties.Items.TryGetValue("client_list", out var item)) return;
        var bytes = Base64Url.Decode(item);
        var value = Encoding.UTF8.GetString(bytes);

        Clients = JsonSerializer.Deserialize<string[]>(value);
    }

    public AuthenticateResult AuthenticateResult { get; }
    public IEnumerable<string>? Clients { get; } = new List<string>();
}