using System.Net;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace Portal.Domain.Options;

[Serializable]
public sealed class ValidationError
{
    public int ErrorCode { get; set; } = (int)HttpStatusCode.BadRequest;
    public string? Message { get; set; } = "Validation failed";

    public List<Dictionary<string, object>> Errors { get; }

    public ValidationError(ValidationResult? validationResult = null)
    {
        Errors = validationResult!.Errors
            .Select(error => new Dictionary<string, object>
            {
                {
                    error.PropertyName, new { message = error.ErrorMessage }
                }
            })
            .ToList();
    }

    public override string ToString() => JsonConvert.SerializeObject(this);
}