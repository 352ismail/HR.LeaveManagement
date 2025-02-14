using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries
{
    public class GetLeaveTypeDetailRequest : IRequest<BaseCommandResponse<LeaveTypeDTO>>
    {
        public int Id { get; set; }
    }
}
