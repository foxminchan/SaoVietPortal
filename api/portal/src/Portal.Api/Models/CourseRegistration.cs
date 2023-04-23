namespace Portal.Api.Models;

/// <summary>
/// Course registration information
/// </summary>
public class CourseRegistration
{
    /// <summary>
    /// Course registration ID
    /// </summary>
    public Guid courseRegistrationId { get; set; }

    /// <summary>
    /// Registration status
    /// </summary>
    /// <example>Chốt</example>
    public string? status { get; set; }

    /// <summary>
    /// Date of registration
    /// </summary>
    /// <example>03/03/2023</example>
    public string? registerDate { get; set; }

    /// <summary>
    /// Appointment date
    /// </summary>
    /// <example>05/03/2023</example>
    public string? appointmentDate { get; set; }

    /// <summary>
    /// Registration fee
    /// </summary>
    /// <example>350000</example>
    public float registerFee { get; set; }

    /// <summary>
    /// Percentage of discount
    /// </summary>
    /// <example>20</example>
    public float discountAmount { get; set; }

    /// <summary>
    /// Payment method ID
    /// </summary>
    public int? paymentMethodId { get; set; }

    /// <summary>
    /// Student ID
    /// </summary>
    public string? studentId { get; set; }

    /// <summary>
    /// Class ID
    /// </summary>
    public string? classId { get; set; }
}