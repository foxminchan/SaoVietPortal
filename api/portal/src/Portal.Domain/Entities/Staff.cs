namespace Portal.Domain.Entities;

public class Staff
{
    public string Id { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public string Dob { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Dsw { get; set; } = string.Empty;
    public int PositionId { get; set; }
    public Position? Position { get; set; }
    public string BranchId { get; set; } = string.Empty;
    public Branch? Branch { get; set; }
    public string ManagerId { get; set; } = string.Empty;
    public Staff? Manager { get; set; }
    public ICollection<Staff> Staffs { get; private set; } = new HashSet<Staff>();
    public ICollection<StudentProgress> StudentProgresses { get; private set; } = new HashSet<StudentProgress>();
    public ICollection<ApplicationUser> Users { get; private set; } = new HashSet<ApplicationUser>();
}