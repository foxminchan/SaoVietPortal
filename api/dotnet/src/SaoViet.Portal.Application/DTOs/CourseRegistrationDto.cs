namespace SaoViet.Portal.Application.DTOs;

public record CourseRegistrationDto(
    Guid Id,
    string Status,
    string RegisterDate,
    string AppointmentDate,
    float Fee,
    float DiscountAmount,
    int? PaymentMethodId,
    string StudentId,
    string ClassId);