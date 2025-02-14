using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, BaseCommandResponse<string>>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IMapper mapper;

        public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
        }
        public async Task<BaseCommandResponse<string>> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.Get(request.Id);
            if (leaveRequest is null)
            {
                return new BaseCommandResponse<string>()
                {
                    Success = false,
                    Message = "Record Not found",
                };
            }
            await leaveRequestRepository.Delete(leaveRequest);
            return new BaseCommandResponse<string>()
            {
                Success = true,
                Message = "Success",
            };

        }
    }
}
