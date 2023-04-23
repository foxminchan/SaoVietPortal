namespace Portal.Domain.Entities;

public class Branch
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public ICollection<Class> Classes { get; private set; } = new HashSet<Class>();
    public ICollection<Staff> Staffs { get; private set; } = new HashSet<Staff>();
    public ICollection<ReceiptsExpenses> ReceiptsExpenses { get; private set; } = new HashSet<ReceiptsExpenses>();
}