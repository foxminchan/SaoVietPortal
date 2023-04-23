namespace Portal.Api.Models;

/// <summary>
/// Course registration information
/// </summary>
public class CourseRegistration
{
    /// <summary>
    /// Course registration ID
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Registration status
    /// </summary>
    /// <example>Chốt</example>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Date of registration
    /// </summary>
    /// <example>03/03/2023</example>
    public string RegisterDate { get; set; } = string.Empty;

    /// <summary>
    /// Appointment date
    /// </summary>
    /// <example>05/03/2023</example>
    public string AppointmentDate { get; set; } = string.Empty;

    /// <summary>
    /// Registration fee
    /// </summary>
    /// <example>350000</example>
    public float Fee { get; set; }

    /// <summary>
    /// Percentage of discount
    /// </summary>
    /// <example>20</example>
    public float DiscountAmount { get; set; }

    /// <summary>
    /// Payment method ID
    /// </summary>
    public int? PaymentMethodId { get; set; }

    /// <summary>
    /// Student ID
    /// </summary>
    public string StudentId { get; set; } = string.Empty;

    /// <summary>
    /// Class ID
    /// </summary>
    public string ClassId { get; set; } = string.Empty;
}