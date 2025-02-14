using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands
{
    public class UpdateLeaveRequestCommand : IRequest<BaseCommandResponse<LeaveRequestDTO>>
    {
        public int Id { get; set; }
        public UpdateLeaveRequestDTO UpdateLeaveRequestDTO { get; set; }
        public ChangeLeaveRequestApprovalDTO ChangeLeaveRequestApprovalDTO { get; set; }
    }
}
