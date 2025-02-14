using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.leaveRequests.Requests.Queries
{
    public class GetLeaveRequestDetailRequest : IRequest<BaseCommandResponse<LeaveRequestDTO>>
    {
        public int Id { get; set; }
    }
}
