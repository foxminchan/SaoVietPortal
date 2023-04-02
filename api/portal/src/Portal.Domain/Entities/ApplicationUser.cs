using Microsoft.AspNetCore.Identity;

namespace Portal.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? imageUrl { get; set; }
    public string? studentId { get; set; }
    public Student? student { get; set; }
    public string? staffId { get; set; }
    public Staff? staff { get; set; }
}