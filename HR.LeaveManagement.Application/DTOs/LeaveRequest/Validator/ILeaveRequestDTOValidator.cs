using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest.Validator
{
    public class ILeaveRequestDTOValidator : AbstractValidator<ILeaveRequestDTO>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public ILeaveRequestDTOValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            RuleFor(p => p.StartDate)
                .NotEmpty()
                .LessThan(x => x.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}");
            RuleFor(p => p.EndDate)
                    .NotEmpty()
                    .GreaterThan(x => x.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}");
            RuleFor(p => p.LeaveTypeId)
                    .NotEmpty()
                    .MustAsync(async (id, token) =>
                    {
                        var leaveTypeExists = await leaveTypeRepository.Exists(id);
                        return leaveTypeExists;
                    }).WithMessage("{PropertyName} does not exist.");
        }
    }
}
