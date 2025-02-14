using HR.LeaveManagement.Application.DTOs.LeaveType;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation
{
    public class LeaveAllocationDTO : BaseDTO, ILeaveAllocationDTO
    {
        public int NumberOfDays { get; set; }
        public int DefaultDays { get; set; }
        public LeaveTypeDTO LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
