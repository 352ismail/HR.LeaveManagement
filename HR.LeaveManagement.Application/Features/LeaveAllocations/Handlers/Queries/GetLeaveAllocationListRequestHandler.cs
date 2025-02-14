using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationListRequestHandler : IRequestHandler<GetLeaveAllocationListRequest, BaseCommandResponse<List<LeaveAllocationDTO>>>
    {
        private readonly ILeaveAllocatedRepository leaveAllocatedRepository;
        private readonly IMapper mapper;

        public GetLeaveAllocationListRequestHandler(ILeaveAllocatedRepository leaveAllocatedRepository, IMapper mapper)
        {
            this.leaveAllocatedRepository = leaveAllocatedRepository;
            this.mapper = mapper;
        }
        public async Task<BaseCommandResponse<List<LeaveAllocationDTO>>> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocations = await leaveAllocatedRepository.GetAll();
            return new BaseCommandResponse<List<LeaveAllocationDTO>>()
            {
                Success = true,
                Message = "Success",
                Data = mapper.Map<List<LeaveAllocationDTO>>(leaveAllocations)
            };
        }
    }
}
