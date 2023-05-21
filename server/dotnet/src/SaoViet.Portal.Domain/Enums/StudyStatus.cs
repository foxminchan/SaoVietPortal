using Ardalis.SmartEnum;

namespace SaoViet.Portal.Domain.Enums;

public sealed class StudyStatus : SmartEnum<StudyStatus>
{
    public static readonly StudyStatus Studying = new(nameof(Studying), 1);
    public static readonly StudyStatus Rejected = new(nameof(Rejected), 2);
    public static readonly StudyStatus Graduate = new(nameof(Graduate), 3);
    public static readonly StudyStatus Postpone = new(nameof(Postpone), 4);

    private StudyStatus(string name, int value) : base(name, value)
    {
    }
}