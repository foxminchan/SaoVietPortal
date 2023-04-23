using Microsoft.AspNetCore.Identity;

namespace Portal.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string ImageUrl { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public Student? Student { get; set; }
    public string StaffId { get; set; } = string.Empty;
    public Staff? Staff { get; set; }
}