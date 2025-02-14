using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse<LeaveAllocationDTO>>
    {
        private readonly ILeaveAllocatedRepository leaveAllocatedRepository;
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public CreateLeaveAllocationCommandHandler(
            ILeaveAllocatedRepository leaveAllocatedRepository,
            IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveAllocatedRepository = leaveAllocatedRepository;
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<BaseCommandResponse<LeaveAllocationDTO>> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocationValidation = new CreateLeaveAllocationDTOValidator(leaveTypeRepository);
            var validationResult = await leaveAllocationValidation.ValidateAsync(request.CreateLeaveAllocationDTO);
            if (!validationResult.IsValid)
            {
                return new BaseCommandResponse<LeaveAllocationDTO>()
                {
                    Success = false,
                    Message = "Creation Failed",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var leaveAllocation = mapper.Map<LeaveAllocation>(request.CreateLeaveAllocationDTO);
            leaveAllocation = await leaveAllocatedRepository.Add(leaveAllocation);
            return new BaseCommandResponse<LeaveAllocationDTO>()
            {
                Success = true,
                Message = "Success",
                Data = mapper.Map<LeaveAllocationDTO>(leaveAllocation)
            };
        }
    }
}
