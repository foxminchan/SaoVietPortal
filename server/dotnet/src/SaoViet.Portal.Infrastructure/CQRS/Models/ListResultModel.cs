using System.Text.Json;

namespace SaoViet.Portal.Infrastructure.CQRS.Models;

public record ListResultModel<T>(List<T>? Items)
    where T : BaseModel
{
    public static ListResultModel<T> Create(List<T>? items) => new(items);

    public override string ToString() => JsonSerializer.Serialize(this);
}