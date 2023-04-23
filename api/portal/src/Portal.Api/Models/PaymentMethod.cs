namespace Portal.Api.Models;

/// <summary>
/// Payment method information
/// </summary>
public class PaymentMethod
{
    /// <summary>
    /// Payment method ID
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Name of payment method
    /// </summary>
    /// <example>Tiền mặt</example>
    public string Name { get; set; } = string.Empty;
}