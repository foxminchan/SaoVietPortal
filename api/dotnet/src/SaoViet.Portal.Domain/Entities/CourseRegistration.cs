using SaoViet.Portal.Domain.AggregateRoot;
using SaoViet.Portal.Domain.Enums;

namespace SaoViet.Portal.Domain.Entities;

public sealed class CourseRegistration : AggregateRoot<CourseRegistrationId>
{
    public CourseRegisterStatus? Status { get; set; }
    public DateOnly RegisterDate { get; set; }
    public DateTime AppointmentDate { get; set; }
    public float Fee { get; set; }
    public byte DiscountAmount { get; set; }
    public PaymentMethodId? PaymentMethodId { get; set; }
    public CourseEnrollmentId? CourseEnrollmentId { get; set; }

    public CourseRegistration()
        : base(new CourseRegistrationId(Guid.NewGuid()))
    { }

    public CourseRegistration(
        CourseRegisterStatus status,
        DateOnly registerDate,
        DateTime appointmentDate,
        float fee,
        byte discountAmount,
        PaymentMethodId paymentMethodId,
        CourseEnrollmentId courseEnrollmentId)
        : base(new CourseRegistrationId(Guid.NewGuid()))
        => (Status, RegisterDate, AppointmentDate, Fee, DiscountAmount, PaymentMethodId, CourseEnrollmentId)
            = (status, registerDate, appointmentDate, fee, discountAmount, paymentMethodId, courseEnrollmentId);

    public CourseEnrollment? CourseEnrollment { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
}

public record CourseRegistrationId(Guid Value);