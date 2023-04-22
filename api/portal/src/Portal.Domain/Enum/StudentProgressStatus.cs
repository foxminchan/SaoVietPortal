using Ardalis.SmartEnum;

namespace Portal.Domain.Enum;

public sealed class StudentProgressStatus : SmartEnum<StudentProgressStatus>
{
    public static readonly StudentProgressStatus ABSENT = new(nameof(ABSENT), 1);
    public static readonly StudentProgressStatus PRESENT = new(nameof(PRESENT), 2);
    public static readonly StudentProgressStatus EXEMPT = new(nameof(EXEMPT), 3);

    private StudentProgressStatus(string name, int value) : base(name, value) { }
}