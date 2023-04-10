namespace Portal.Api.Models;

/// <summary>
/// Phương thức thanh toán
/// </summary>
public class PaymentMethod
{
    /// <summary>
    /// Mã phương thức thanh toán
    /// </summary>
    public int paymentMethodId { get; set; }

    /// <summary>
    /// Tên phương thức thanh toán
    /// </summary>
    /// <example>Tiền mặt</example>
    public string? paymentMethodName { get; set; }
}