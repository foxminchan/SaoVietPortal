namespace Portal.Api.Models;

public record CourseRegistrationResponse(
    Guid Id,
    string Status,
    string RegisterDate,
    string AppointmentDate,
    float Fee,
    float DiscountAmount,
    int? PaymentMethodId,
    string StudentId,
    string ClassId)
{
    public CourseRegistrationResponse() : this(Guid.NewGuid(), string.Empty, string.Empty, string.Empty, 0, 0, null, string.Empty, string.Empty) { }
}