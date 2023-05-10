using System.Text.Json;

namespace Portal.Shared.Infrastructure.Common;

public record ResultModel<T>(T Data, bool IsError = false, string ErrorMessage = default!) where T : notnull
{
    public static ResultModel<T> Create(T data, bool isError = false, string errorMessage = default!) =>
        new(data, isError, errorMessage);

    public override string ToString() => JsonSerializer.Serialize(this);
}