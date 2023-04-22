using Ardalis.SmartEnum;

namespace Portal.Domain.Enum;

public sealed class CourseRegistrationStatus : SmartEnum<CourseRegistrationStatus>
{
    public static readonly CourseRegistrationStatus CLOSING = new(nameof(CLOSING), 1);
    public static readonly CourseRegistrationStatus APPOINTMENT = new(nameof(APPOINTMENT), 2);
    public static readonly CourseRegistrationStatus DENIED = new(nameof(DENIED), 3);

    private CourseRegistrationStatus(string name, int value) : base(name, value)
    {
    }
}