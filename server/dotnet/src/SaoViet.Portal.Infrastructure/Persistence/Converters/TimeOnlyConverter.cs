using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SaoViet.Portal.Infrastructure.Persistence.Converters;

public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
{
    public TimeOnlyConverter() : base(
        timeOnly => timeOnly.ToTimeSpan(),
        timeSpan => TimeOnly.FromTimeSpan(timeSpan))
    { }
}