﻿using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries
{
    public class GetLeaveAllocationListRequest : IRequest<BaseCommandResponse<List<LeaveAllocationDTO>>>
    {
    }
}
