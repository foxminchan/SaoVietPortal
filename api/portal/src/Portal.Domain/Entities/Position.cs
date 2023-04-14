namespace Portal.Domain.Entities;

public class Position
{
    public int? positionId { get; set; }
    public string? positionName { get; set; }
    public List<Staff>? staffs { get; set; }
}