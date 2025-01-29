using FluentValidation;

namespace HR.LeaveManagement.Application.DTOs.LeaveType.Validators
{
    public class ILeaveTypeDTOValidator : AbstractValidator<ILeaveTypeDTO>
    {
        public ILeaveTypeDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed {MaxLength} characters");

            RuleFor(x => x.DefaultDays)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must not exceed {ComparisonValue}");
        }
    }
}
