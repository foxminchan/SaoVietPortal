namespace Portal.Api.Models;

public record PaymentMethodResponse(int? Id, string Name)
{
    public PaymentMethodResponse() : this(null, string.Empty) { }
}