using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Portal.Infrastructure.Helpers
{
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
}
