using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands
{
    public class DeleteLeaveRequestCommand : IRequest<BaseCommandResponse<string>>
    {
        public int Id { get; set; }
    }
}
