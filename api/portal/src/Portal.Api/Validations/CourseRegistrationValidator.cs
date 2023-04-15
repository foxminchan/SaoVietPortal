using System.Globalization;
using FluentValidation;
using Portal.Api.Models;

namespace Portal.Api.Validations;

public class CourseRegistrationValidator : AbstractValidator<CourseRegistration>
{
    public CourseRegistrationValidator()
    {
        RuleFor(x => x.registerDate)
            .Must(registerDate => DateTime.TryParseExact(registerDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            .WithMessage("Register date is not valid");
        RuleFor(x => x.appointmentDate)
            .Must((courseRegistration, appointmentDate) => DateTime.TryParseExact(appointmentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedAppointmentDate)
                                                                     && DateTime.TryParseExact(courseRegistration.registerDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedRegisterDate) && parsedAppointmentDate >= parsedRegisterDate)
            .WithMessage("Appointment date must be greater than or equal to register date");
        RuleFor(x => x.registerFee)
            .GreaterThan(0).WithMessage("Register fee must be greater than 0");
        RuleFor(x => x.discountAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Discount amount must be greater than or equal to 0")
            .LessThanOrEqualTo(100).WithMessage("Discount amount must be less than or equal to 100");
        RuleFor(x => x.status)
            .IsInEnum().WithMessage("Status must be one of the following: 'CLOSING', 'APPOINTMENT', 'DENIED'");
    }
}