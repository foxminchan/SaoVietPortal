using System.Text.Json;

namespace Portal.Domain.Entities;

public class Student
{
    public string Id { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public bool Gender { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Dob { get; set; } = string.Empty;
    public string Pod { get; set; } = string.Empty;
    public string Occupation { get; set; } = string.Empty;
    public JsonElement? SocialNetwork { get; set; }
    public ICollection<ApplicationUser>? Users { get; private set; } = new HashSet<ApplicationUser>();
    public ICollection<CourseEnrollment>? CourseEnrollments { get; private set; } = new HashSet<CourseEnrollment>();
}