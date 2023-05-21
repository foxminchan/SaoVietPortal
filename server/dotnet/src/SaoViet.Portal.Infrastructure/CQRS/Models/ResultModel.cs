using System.Text.Json;

namespace SaoViet.Portal.Infrastructure.CQRS.Models;

public record ResultModel<T>(T? Data, bool IsError = false, string? ErrorMessage = default)
    where T : notnull
{
    public static ResultModel<T> Create(T? data, bool isError = false, string? errorMessage = default)
        => new(data, isError, errorMessage);

    public static ResultModel<T> CreateError(T? data, string? errorMessage = default)
        => new(default, true, errorMessage);

    public override string ToString() => JsonSerializer.Serialize(this);
}