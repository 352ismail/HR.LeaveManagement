using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationDetailRequestHandler : IRequestHandler<GetLeaveAllocationDetailRequest, BaseCommandResponse<LeaveAllocationDTO>>
    {
        private readonly ILeaveAllocatedRepository leaveAllocatedRepository;
        private readonly IMapper mapper;

        public GetLeaveAllocationDetailRequestHandler(ILeaveAllocatedRepository leaveAllocatedRepository, IMapper mapper)
        {
            this.leaveAllocatedRepository = leaveAllocatedRepository;
            this.mapper = mapper;
        }
        public async Task<BaseCommandResponse<LeaveAllocationDTO>> Handle(GetLeaveAllocationDetailRequest request, CancellationToken cancellationToken)
        {

            var leaveAllocation = await leaveAllocatedRepository.Get(request.Id);
            if (leaveAllocation is null)
            {
                return new BaseCommandResponse<LeaveAllocationDTO>()
                {
                    Success = false,
                    Message = "Record Not found",
                };
            }

            return new BaseCommandResponse<LeaveAllocationDTO>()
            {
                Success = true,
                Message = "Success",
                Data = mapper.Map<LeaveAllocationDTO>(leaveAllocation)
            };
        }
    }
}
