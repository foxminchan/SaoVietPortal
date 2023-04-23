namespace Portal.Domain.Entities;

public class Course
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<Class> Classes { get; set; } = new HashSet<Class>();
}