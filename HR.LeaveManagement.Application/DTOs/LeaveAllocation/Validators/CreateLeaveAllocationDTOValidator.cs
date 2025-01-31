using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
    public class CreateLeaveAllocationDTOValidator : AbstractValidator<CreateLeaveAllocationDTO>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public CreateLeaveAllocationDTOValidator(ILeaveTypeRepository leaveTypeRepository)

        {
            this.leaveTypeRepository = leaveTypeRepository;
            Include(new ILeaveAllocationDTOValidator(leaveTypeRepository));
        }
    }
}
