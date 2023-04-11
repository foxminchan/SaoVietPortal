namespace Portal.Api.Models;

/// <summary>
/// Thông tin đăng ký khoá học
/// </summary>
public class CourseRegistration
{
    /// <summary>
    /// Mã phiếu đăng ký khoá học
    /// </summary>
    public Guid courseRegistrationId { get; set; }

    /// <summary>
    /// Trạng thái đăng ký khoá học
    /// </summary>
    /// <example>Chốt</example>
    public string? status { get; set; }

    /// <summary>
    /// Ngày đăng ký khoá học
    /// </summary>
    public string? registerDate { get; set; }

    /// <summary>
    /// Ngày hẹn đăng ký khoá học
    /// </summary>
    public string? appointmentDate { get; set; }

    /// <summary>
    /// Học phí đăng ký khoá học
    /// </summary>
    public float registerFee { get; set; }

    /// <summary>
    /// Phần trăm giảm giá
    /// </summary>
    public float discountAmount { get; set; }

    /// <summary>
    /// Mã hình thức thanh toán
    /// </summary>
    public int? paymentMethodId { get; set; }

    /// <summary>
    /// Mã học viên
    /// </summary>
    public string? studentId { get; set; }

    /// <summary>
    /// Mã lớp học
    /// </summary>
    public string? classId { get; set; }
}