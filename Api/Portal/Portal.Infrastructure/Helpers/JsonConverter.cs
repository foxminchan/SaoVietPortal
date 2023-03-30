using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Portal.Infrastructure.Helpers;

public class JsonConverter : ValueConverter<JsonElement, string>
{
    public JsonConverter() : base(
        v => v.ToString(),
        v => JsonDocument.Parse(v,
            new JsonDocumentOptions
            {
                AllowTrailingCommas = true,
                CommentHandling = JsonCommentHandling.Skip
            }
        ).RootElement
    )
    {
    }
}