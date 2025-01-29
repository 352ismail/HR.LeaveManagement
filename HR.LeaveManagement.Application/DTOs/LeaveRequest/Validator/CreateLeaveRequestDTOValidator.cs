﻿using FluentValidation;
using HR.LeaveManagement.Application.Persistense.Contracts;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest.Validator
{
    public class CreateLeaveRequestDTOValidator : AbstractValidator<CreateLeaveRequestDTO>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public CreateLeaveRequestDTOValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            Include(new ILeaveRequestDTOValidator(leaveTypeRepository));
            this.leaveTypeRepository = leaveTypeRepository;
        }
    }

}
