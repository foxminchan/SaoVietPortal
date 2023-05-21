using SaoViet.Portal.Domain.AggregateRoot;
using SaoViet.Portal.Domain.ValueObjects;

namespace SaoViet.Portal.Domain.Entities;

public sealed class Staff : AggregateRoot<StaffId>
{
    public string? Fullname { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Address? Address { get; set; }
    public DateOnly DateStartWork { get; set; }
    public BranchId? BranchId { get; set; }
    public StaffId? ManagerId { get; set; }
    public PositionId? PositionId { get; set; }

    public Position? Position { get; set; }
    public Staff? Manager { get; set; }
    public Branch? Branch { get; set; }

    public Staff() : base(new StaffId(Guid.NewGuid()))
    { }

    public Staff(
        string? fullname,
        DateOnly dateOfBirth,
        Address? address,
        DateOnly dateStartWork,
        Position? position,
        BranchId branchId,
        PositionId positionId,
        StaffId managerId) : base(new StaffId(Guid.NewGuid()))
        => (Fullname, DateOfBirth, Address, DateStartWork, Position, BranchId, PositionId, ManagerId)
            = (fullname, dateOfBirth, address, dateStartWork, position, branchId, positionId, managerId);

    public HashSet<Staff> Staffs { get; private set; } = new();
    public HashSet<StudentProgress> StudentProgresses { get; private set; } = new();
    public HashSet<ApplicationUser> Users { get; private set; } = new();
}

public record StaffId(Guid Value);