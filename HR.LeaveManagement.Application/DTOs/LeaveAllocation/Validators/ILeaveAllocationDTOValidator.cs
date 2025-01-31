using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
    public class ILeaveAllocationDTOValidator : AbstractValidator<ILeaveAllocationDTO>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public ILeaveAllocationDTOValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            //RuleFor(x => x.DefaultDays)
            //  .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.")
            //  .NotNull().WithMessage("{PropertyName} is required.")
            //  .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.Period)
                .GreaterThanOrEqualTo(x => DateTime.Now.Year).WithMessage("{PropertyName} must be equal after {ComparisonValue}.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.NumberOfDays)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.LeaveTypeId)
                .NotNull().WithMessage("{PropertyName} is required")
            .MustAsync(async (id, token) =>
            {
                var leaveTypeId = await leaveTypeRepository.Exists(id);
                return !leaveTypeId;
            }).WithMessage("""{PropertyName} does not exist.""");
        }
    }
}

