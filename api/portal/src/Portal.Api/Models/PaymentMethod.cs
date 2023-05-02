namespace Portal.Api.Models;

public record PaymentMethod(int? Id, string Name)
{
    public PaymentMethod() : this(null, string.Empty) { }
}