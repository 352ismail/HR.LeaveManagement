namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation
{
    public class CreateLeaveAllocationDTO : ILeaveAllocationDTO
    {
        public int NumberOfDays { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
