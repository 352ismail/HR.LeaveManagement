using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, BaseCommandResponse<string>>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
        }

        public async Task<BaseCommandResponse<string>> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await this.leaveTypeRepository.Get(request.Id);
            if (leaveType is null)
            {
                return new BaseCommandResponse<string>()
                {
                    Success = false,
                    Message = "Record Not found",
                };
            }
            await this.leaveTypeRepository.Delete(leaveType);
            return new BaseCommandResponse<string>()
            {
                Success = true,
                Message = "Success",
            };
        }
    }
}
