using Ardalis.SmartEnum;

namespace SaoViet.Portal.Domain.Enums;

public sealed class CourseRegisterStatus : SmartEnum<CourseRegisterStatus>
{
    public static readonly CourseRegisterStatus Pending = new(nameof(Pending), 1);
    public static readonly CourseRegisterStatus Approved = new(nameof(Approved), 2);
    public static readonly CourseRegisterStatus Rejected = new(nameof(Rejected), 3);

    private CourseRegisterStatus(string name, int value) : base(name, value)
    {
    }
}