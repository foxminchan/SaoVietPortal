using Ardalis.SmartEnum;

namespace SaoViet.Portal.Domain.Enums;

public class StudentProgressStatus : SmartEnum<StudentProgressStatus>
{
    public static readonly StudentProgressStatus Completed = new(nameof(Completed), 1);
    public static readonly StudentProgressStatus Absent = new(nameof(Absent), 2);
    public static readonly StudentProgressStatus Exempt = new(nameof(Exempt), 3);

    private StudentProgressStatus(string name, int value) : base(name, value)
    {
    }
}