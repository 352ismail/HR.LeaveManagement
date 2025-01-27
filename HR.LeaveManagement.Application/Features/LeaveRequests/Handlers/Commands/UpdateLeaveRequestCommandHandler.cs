using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Persistense.Contracts;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    internal class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IMapper mapper;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var existingLeaveRequest = await leaveRequestRepository.Get(request.Id);
            if (request.UpdateLeaveRequestDTO is not null)
            {

                mapper.Map(request.UpdateLeaveRequestDTO, existingLeaveRequest);
                await leaveRequestRepository.Update(existingLeaveRequest);
            }
            else if (request.ChangeLeaveRequestApprovalDTO is not null)
            {
                mapper.Map(request.ChangeLeaveRequestApprovalDTO, existingLeaveRequest);
                await leaveRequestRepository.ChangeApprovalStatus(existingLeaveRequest, request.ChangeLeaveRequestApprovalDTO.Approved);
            }
            return Unit.Value;
        }
    }
}
