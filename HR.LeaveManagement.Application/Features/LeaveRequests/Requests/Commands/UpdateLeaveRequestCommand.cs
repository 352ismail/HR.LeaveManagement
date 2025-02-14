using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands
{
    public class UpdateLeaveRequestCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public UpdateLeaveRequestDTO UpdateLeaveRequestDTO { get; set; }
        public ChangeLeaveRequestApprovalDTO ChangeLeaveRequestApprovalDTO { get; set; }
    }
}
