using Microsoft.AspNetCore.Identity;

namespace SaoViet.Portal.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? AvatarUrl { get; set; }
    public StudentId? StudentId { get; set; }
    public StaffId? StaffId { get; set; }

    public Student? Student { get; set; }
    public Staff? Staff { get; set; }
}