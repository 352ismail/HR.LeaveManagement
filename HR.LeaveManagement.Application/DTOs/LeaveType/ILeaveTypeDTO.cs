namespace HR.LeaveManagement.Application.DTOs.LeaveType
{
    public interface ILeaveTypeDTO
    {
        public string Name { get; set; }
        public int DefaultDays { get; set; }
    }
}
