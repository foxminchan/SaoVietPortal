namespace Portal.Domain.Entities;

public class Position
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Staff> Staffs { get; private set; } = new HashSet<Staff>();
}