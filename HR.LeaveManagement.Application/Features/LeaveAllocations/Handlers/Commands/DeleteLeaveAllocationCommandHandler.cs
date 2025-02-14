using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, BaseCommandResponse<string>>
    {
        private readonly ILeaveAllocatedRepository leaveAllocatedRepository;
        private readonly IMapper mapper;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocatedRepository leaveAllocatedRepository, IMapper mapper)
        {
            this.leaveAllocatedRepository = leaveAllocatedRepository;
            this.mapper = mapper;
        }
        public async Task<BaseCommandResponse<string>> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await leaveAllocatedRepository.Get(request.Id);
            if (leaveAllocation is null)
            {
                return new BaseCommandResponse<string>()
                {
                    Success = false,
                    Message = "Record Not found",
                };
            }
            await leaveAllocatedRepository.Delete(leaveAllocation);
            return new BaseCommandResponse<string>()
            {
                Success = true,
                Message = "Success",
            };

        }
    }
}
