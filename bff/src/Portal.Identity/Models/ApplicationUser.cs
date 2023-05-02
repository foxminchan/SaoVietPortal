using Microsoft.AspNetCore.Identity;

namespace Portal.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string ImageUrl { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public string StaffId { get; set; } = string.Empty;
}