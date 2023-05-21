namespace SaoViet.Portal.Application.PaymentMethod.DTO;

public record PaymentMethodDto(int? Id, string Name)
{
    public PaymentMethodDto() : this(null, string.Empty) { }
}