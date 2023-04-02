namespace Portal.Domain.Entities;

public class Course
{
    public string? courseId { get; set; }
    public string? courseName { get; set; }
    public string? description { get; set; }
    public List<Class>? classes { get; set; }
}