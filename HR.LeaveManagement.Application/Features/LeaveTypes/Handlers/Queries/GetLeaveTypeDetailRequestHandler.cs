using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries
{
    public class GetLeaveTypeDetailRequestHandler : IRequestHandler<GetLeaveTypeDetailRequest, BaseCommandResponse<LeaveTypeDTO>>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;

        public GetLeaveTypeDetailRequestHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
        }
        public async Task<BaseCommandResponse<LeaveTypeDTO>> Handle(GetLeaveTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveType = await leaveTypeRepository.Get(request.Id);
            if (leaveType is null)
            {
                return new BaseCommandResponse<LeaveTypeDTO>()
                {
                    Success = false,
                    Message = "Record Not found",
                };
            }
            return new BaseCommandResponse<LeaveTypeDTO>()
            {
                Success = true,
                Message = "Success",
                Data = mapper.Map<LeaveTypeDTO>(leaveType)
            };
        }
    }

}
