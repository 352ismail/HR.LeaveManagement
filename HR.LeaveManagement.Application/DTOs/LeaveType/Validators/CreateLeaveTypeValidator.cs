using FluentValidation;

namespace HR.LeaveManagement.Application.DTOs.LeaveType.Validators
{
    public class CreateLeaveTypeDTOValidator : AbstractValidator<CreateLeaveTypeDTO>
    {
        public CreateLeaveTypeDTOValidator()
        {
            Include(new ILeaveTypeDTOValidator());
        }
    }
}
