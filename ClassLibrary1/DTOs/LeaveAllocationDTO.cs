﻿namespace HR.LeaveManagement.Application.DTOs
{
    public class LeaveAllocationDTO : BaseDTO
    {
        public int NumberOfDays { get; set; }
        public int DefaultDays { get; set; }
        public LeaveTypeDTO leaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
