using System.Globalization;
using FluentValidation;
using Portal.Api.Models;
using Portal.Domain.Interfaces.Common;

namespace Portal.Api.Validations;

public class CourseRegistrationValidator : AbstractValidator<CourseRegistration>
{
    public CourseRegistrationValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.RegisterDate)
            .Must(registerDate => DateTime.TryParseExact(registerDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            .WithMessage("Register date is not valid");
        RuleFor(x => x.AppointmentDate)
            .Must((courseRegistration, appointmentDate) => DateTime.TryParseExact(appointmentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedAppointmentDate)
                                                                     && DateTime.TryParseExact(courseRegistration.RegisterDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedRegisterDate) && parsedAppointmentDate >= parsedRegisterDate)
            .WithMessage("Appointment date must be greater than or equal to register date");
        RuleFor(x => x.Fee)
            .GreaterThan(0).WithMessage("Register fee must be greater than 0");
        RuleFor(x => x.DiscountAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Discount amount must be greater than or equal to 0")
            .LessThanOrEqualTo(100).WithMessage("Discount amount must be less than or equal to 100");
        RuleFor(x => x.PaymentMethodId)
            .Must((_, paymentMethodId) => paymentMethodId.HasValue
                                        || unitOfWork.PaymentMethodRepository.TryGetPaymentMethod(paymentMethodId, out var _))
            .WithMessage("Payment method with id {PropertyValue} does not exist");
        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Student id is required")
            .Must((_, studentId) => studentId is null
                                   || unitOfWork.StudentRepository.TryGetStudentById(studentId, out var _))
            .WithMessage("Student with id {PropertyValue} does not exist");
        RuleFor(x => x.ClassId)
            .NotEmpty().WithMessage("Class id is required")
            .Must((_, classId) => classId is null
                                            || unitOfWork.ClassRepository.TryGetClassById(classId, out var _))
            .WithMessage("Class with id {PropertyValue} does not exist");
    }
}