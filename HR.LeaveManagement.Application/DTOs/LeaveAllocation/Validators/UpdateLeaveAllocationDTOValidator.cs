using FluentValidation;
using HR.LeaveManagement.Application.Persistense.Contracts;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
    internal class UpdateLeaveAllocationDTOValidator : AbstractValidator<UpdateLeaveAllocationDTO>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public UpdateLeaveAllocationDTOValidator(ILeaveTypeRepository leaveTypeRepository)

        {
            this.leaveTypeRepository = leaveTypeRepository;
            Include(new ILeaveAllocationDTOValidator(leaveTypeRepository));
        }
    }
}
