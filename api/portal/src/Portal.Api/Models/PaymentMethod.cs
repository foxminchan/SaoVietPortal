namespace Portal.Api.Models;

/// <summary>
/// Payment method information
/// </summary>
public class PaymentMethod
{
    /// <summary>
    /// Payment method ID
    /// </summary>
    public int? paymentMethodId { get; set; }

    /// <summary>
    /// Name of payment method
    /// </summary>
    /// <example>Tiền mặt</example>
    public string? paymentMethodName { get; set; }
}