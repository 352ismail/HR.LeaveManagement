namespace HR.LeaveManagement.Application.DTOs
{
    public class LeaveTypeDTO : BaseDTO
    {
        public string Name { get; set; }
        public int DefaultDays { get; set; }
    }
}
