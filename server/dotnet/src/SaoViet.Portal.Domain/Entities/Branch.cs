using SaoViet.Portal.Domain.AggregateRoot;
using SaoViet.Portal.Domain.Enums;
using SaoViet.Portal.Domain.ValueObjects;

namespace SaoViet.Portal.Domain.Entities;

public sealed class Branch : AggregateRoot<BranchId>
{
    public string? Name { get; set; }
    public Address? Address { get; set; }
    public string? Phone { get; set; }
    public BranchStatus? Status { get; set; }

    public Branch() : base(new BranchId(string.Empty))
    { }

    public Branch(
        BranchId id,
        string? name,
        Address? address,
        string? phone) : base(id)
        => (Name, Address, Phone, Status) = (name, address, phone, BranchStatus.Active);

    public HashSet<Class> Classes { get; private set; } = new();
    public HashSet<Staff> Staffs { get; private set; } = new();
    public HashSet<ReceiptsExpenses> ReceiptsExpenses { get; private set; } = new();
}

public record BranchId(string Value);