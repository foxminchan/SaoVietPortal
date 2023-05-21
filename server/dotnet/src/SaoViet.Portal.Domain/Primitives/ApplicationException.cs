namespace SaoViet.Portal.Domain.Primitives;

public class ApplicationException : Exception
{
    public ApplicationException(string message) : base(message)
    {
    }

    public ApplicationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public static ApplicationException Exception(string message) => new(message);

    public static ApplicationException NullArgument(string arg) => new($"Argument {arg} cannot be null.");

    public static ApplicationException InvalidArgument(string arg) => new($"Argument {arg} is invalid.");

    public static ApplicationException InvalidOperation(string opr) => new($"Operation {opr} is invalid.");

    public static ApplicationException NotFound(string arg) => new($"{arg} not found.");
}