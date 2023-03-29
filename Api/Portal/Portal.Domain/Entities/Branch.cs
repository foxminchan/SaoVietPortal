namespace Portal.Domain.Entities;

public class Branch
{
    public string? branchId { get; set; }
    public string? branchName { get; set; }
    public string? address { get; set; }
    public string? phone { get; set; }
    public List<Class>? classes { get; set; }
    public List<Staff>? staffs { get; set; }
    public List<ReceiptsExpenses>? receiptsExpenses { get; set; }
}