namespace Portal.Domain.Entities;

public class Staff
{
    public string? staffId { get; set; }
    public string? fullname { get; set; }
    public string? dob { get; set; }
    public string? address { get; set; }
    public string? dsw { get; set; }
    public int positionId { get; set; }
    public Position? position { get; set; }
    public string? branchId { get; set; }
    public Branch? branch { get; set; }
    public string? managerId { get; set; }
    public Staff? manager { get; set; }
    public List<Staff>? staffs { get; set; }
    public List<StudentProgress>? studentProgresses { get; set; }
    public List<ApplicationUser>? users { get; set; }
}