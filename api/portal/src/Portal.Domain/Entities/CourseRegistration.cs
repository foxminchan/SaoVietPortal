namespace Portal.Domain.Entities;

public class CourseRegistration
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public string RegisterDate { get; set; } = string.Empty;
    public string AppointmentDate { get; set; } = string.Empty;
    public float Fee { get; set; }
    public float DiscountAmount { get; set; }
    public int PaymentMethodId { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public string StudentId { get; set; } = string.Empty;
    public string ClassId { get; set; } = string.Empty;
    public CourseEnrollment? CourseEnrollment { get; set; }
}