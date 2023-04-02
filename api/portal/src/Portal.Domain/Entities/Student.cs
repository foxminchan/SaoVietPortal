using System.Text.Json;

namespace Portal.Domain.Entities;

public class Student
{
    public string? studentId { get; set; }
    public string? fullname { get; set; }
    public bool gender { get; set; }
    public string? address { get; set; }
    public string? dob { get; set; }
    public string? pod { get; set; }
    public string? occupation { get; set; }
    public JsonElement? socialNetwork { get; set; }
    public List<ApplicationUser>? users { get; set; }
    public List<CourseEnrollment>? courseEnrollments { get; set; }
}