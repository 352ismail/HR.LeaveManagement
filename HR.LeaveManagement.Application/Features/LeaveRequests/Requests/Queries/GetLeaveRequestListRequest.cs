using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.leaveRequests.Requests.Queries
{
    public class GetLeaveRequestListRequest : IRequest<BaseCommandResponse<List<LeaveRequestListDTO>>>
    {
    }
}
