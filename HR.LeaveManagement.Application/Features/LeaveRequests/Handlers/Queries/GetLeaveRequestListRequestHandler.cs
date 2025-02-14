using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.leaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.leaveRequests.Handlers.Queries
{
    public class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, BaseCommandResponse<List<LeaveRequestListDTO>>>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;

        public GetLeaveRequestListRequestHandler(ILeaveRequestRepository leaveRequestRepository)
        {
            this.leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<BaseCommandResponse<List<LeaveRequestListDTO>>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.GetLeaveRequestWithDetails();
            return new BaseCommandResponse<List<LeaveRequestListDTO>>()
            {
                Success = true,
                Message = "Success",
                Data = leaveRequest
            };
        }
    }
}
