using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Portal.Infrastructure.Helpers;

public class StringConverter : ValueConverter<string, DateTime>
{
    public StringConverter() : base(
        v => DateTime.Parse(v),
        v => v.ToString("dd/MM/yyyy")
    )
    {
    }
}