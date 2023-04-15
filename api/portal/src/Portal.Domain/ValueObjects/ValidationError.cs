using FluentValidation.Results;
using System.Net;
using Newtonsoft.Json;

namespace Portal.Domain.ValueObjects;

public sealed class ValidationError
{
    public int errorCode { get; set; } = (int)HttpStatusCode.BadRequest;
    public string? message { get; set; } = "Validation failed";

    public List<Dictionary<string, object>> Errors { get; }

    public ValidationError(ValidationResult? validationResult = null)
    {
        Errors = validationResult!.Errors
            .Select(error => new Dictionary<string, object> {
                { error.PropertyName, new
                    { message = error.ErrorMessage }
                }
            })
            .ToList(); ;
    }

    public override string ToString() => JsonConvert.SerializeObject(this);
}