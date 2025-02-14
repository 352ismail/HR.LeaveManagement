using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.leaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.leaveRequests.Handlers.Queries
{
    public class GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, BaseCommandResponse<LeaveRequestDTO>>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IMapper mapper;

        public GetLeaveRequestDetailRequestHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
        }
        public async Task<BaseCommandResponse<LeaveRequestDTO>> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
            if (leaveRequest is null)
            {
                return new BaseCommandResponse<LeaveRequestDTO>()
                {
                    Success = false,
                    Message = "Record Not found",
                };
            }
            return new BaseCommandResponse<LeaveRequestDTO>()
            {
                Success = true,
                Message = "Success",
                Data = leaveRequest
            };


        }

    }
}
