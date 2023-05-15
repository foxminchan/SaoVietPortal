namespace SaoViet.Portal.Application.DTOs;

public record PaymentMethodDto(int? Id, string Name)
{
    public PaymentMethodDto() : this(null, string.Empty) { }
}