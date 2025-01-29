namespace HR.LeaveManagement.Application.DTOs.LeaveType
{
    public class UpdateLeaveTypeDTO : BaseDTO, ILeaveTypeDTO
    {
        public string Name { get; set; }
        public int DefaultDays { get; set; }
    }
}
