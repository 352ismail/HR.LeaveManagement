using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    internal class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, BaseCommandResponse<LeaveTypeDTO>>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
        }
        public async Task<BaseCommandResponse<LeaveTypeDTO>> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveTypeDTOValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateLeaveTypeDTO);
            if (!validationResult.IsValid)
            {
                return new BaseCommandResponse<LeaveTypeDTO>()
                {
                    Success = false,
                    Message = "Creation Failed",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var leaveType = await leaveTypeRepository.Get(request.UpdateLeaveTypeDTO.Id);
            if (leaveType is null)
            {
                return new BaseCommandResponse<LeaveTypeDTO>()
                {
                    Success = false,
                    Message = "Record Not Found.",
                };
            }
            mapper.Map(request.UpdateLeaveTypeDTO, leaveType);
            await leaveTypeRepository.Update(leaveType);
            return new BaseCommandResponse<LeaveTypeDTO>()
            {
                Success = true,
                Data = mapper.Map<LeaveTypeDTO>(leaveType),
                Message = "Success.",
            };

        }
    }
}
