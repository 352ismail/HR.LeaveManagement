using FluentValidation;
using HR.LeaveManagement.Application.Persistense.Contracts;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest.Validator
{
    public class UpdateLeaveRequestDTOValidator : AbstractValidator<ILeaveRequestDTO>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public UpdateLeaveRequestDTOValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            Include(new ILeaveRequestDTOValidator(leaveTypeRepository));
        }
    }
}
