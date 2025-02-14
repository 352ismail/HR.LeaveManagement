using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse<LeaveTypeDTO>>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;

        public CreateLeaveTypeCommandHandler(
            ILeaveTypeRepository leaveTypeRepository,
            IMapper mapper)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
        }
        public async Task<BaseCommandResponse<LeaveTypeDTO>> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveTypeDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreateLeaveTypeDTO);
            if (!validationResult.IsValid)
            {
                return new BaseCommandResponse<LeaveTypeDTO>()
                {
                    Success = false,
                    Message = "Creation Failed",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var leaveType = mapper.Map<LeaveType>(request.CreateLeaveTypeDTO);
            leaveType = await leaveTypeRepository.Add(leaveType);
            return new BaseCommandResponse<LeaveTypeDTO>()
            {
                Success = true,
                Message = "Success",
                Data = mapper.Map<LeaveTypeDTO>(leaveType)
            };
        }
    }
}
