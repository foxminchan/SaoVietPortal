namespace SaoViet.Portal.Infrastructure.Validator;

public class ValidationError
{
    public string? Field { get; }
    public string? Message { get; }

    public ValidationError(string field, string message) => (Field, Message) = (field, message);
}