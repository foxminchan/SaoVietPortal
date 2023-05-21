using SaoViet.Portal.Domain.AggregateRoot;

namespace SaoViet.Portal.Domain.Entities;

public sealed class Class : AggregateRoot<ClassId>
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public float Fee { get; set; }
    public BranchId? BranchId { get; set; }

    public Class() : base(new ClassId(Guid.NewGuid()))
    { }

    public Class(
        DateOnly startDate,
        DateOnly endDate,
        float fee,
        BranchId? branchId)
        : base(new ClassId(Guid.NewGuid()))
        => (StartDate, EndDate, Fee, BranchId)
            = (startDate, endDate, fee, branchId);

    public Branch? Branch { get; set; }
    public Course? Course { get; set; }

    public HashSet<CourseEnrollment> CourseEnrollments { get; private set; } = new();
    public List<Curriculum> Curricula { get; private set; } = new();
}

public record ClassId(Guid Value);