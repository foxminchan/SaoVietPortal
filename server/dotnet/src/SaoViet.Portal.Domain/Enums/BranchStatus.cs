using Ardalis.SmartEnum;

namespace SaoViet.Portal.Domain.Enums;

public sealed class BranchStatus : SmartEnum<BranchStatus>
{
    public static readonly BranchStatus Active = new(nameof(Active), 1);
    public static readonly BranchStatus Suspended = new(nameof(Suspended), 2);
    public static readonly BranchStatus Closed = new(nameof(Closed), 3);

    private BranchStatus(string name, int value) : base(name, value)
    {
    }
}