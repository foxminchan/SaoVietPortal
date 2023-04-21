using System.Globalization;
using FluentValidation;
using Portal.Api.Models;
using Portal.Domain.Interfaces.Common;

namespace Portal.Api.Validations;

public class StaffValidator : AbstractValidator<Staff>
{
    public StaffValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.staffId)
            .NotEmpty().WithMessage("StaffId is required")
            .MaximumLength(20).WithMessage("StaffId must not exceed 20 characters");
        RuleFor(x => x.fullname)
            .NotEmpty().WithMessage("Fullname is required")
            .MaximumLength(50).WithMessage("Fullname must not exceed 50 characters");
        RuleFor(x => x.dob)
            .Must(dobString =>
            {
                if (!DateTime.TryParseExact(dobString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dob))
                    return false;
                return dob < DateTime.Now;
            }).WithMessage("Date of birth must be in the past");
        RuleFor(x => x.address)
            .MaximumLength(80).WithMessage("Address must not exceed 80 characters");
        RuleFor(x => x.managerId)
            .Must((_, managerId) => managerId is null || unitOfWork.staffRepository.TryGetStaffById(managerId, out var _))
            .WithMessage("Manager with id {PropertyValue} does not exist");
        RuleFor(x => x.branchId)
            .Must((_, branchId) => branchId is null || unitOfWork.branchRepository.TryGetBranchById(branchId, out var _))
            .WithMessage("Branch with id {PropertyValue} does not exist");
        RuleFor(x => x.positionId)
            .Must((_, positionId) => !positionId.HasValue ||
                                     unitOfWork.positionRepository.TryGetPositionById(positionId.Value, out var _))
            .WithMessage("Position with id {PropertyValue} does not exist");
    }
}