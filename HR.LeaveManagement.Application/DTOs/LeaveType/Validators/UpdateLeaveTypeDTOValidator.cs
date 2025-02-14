using FluentValidation;

namespace HR.LeaveManagement.Application.DTOs.LeaveType.Validators
{
    public class UpdateLeaveTypeDTOValidator : AbstractValidator<UpdateLeaveTypeDTO>
    {
        public UpdateLeaveTypeDTOValidator()
        {
            Include(new ILeaveTypeDTOValidator());
            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} is required");
        }
    }
}

