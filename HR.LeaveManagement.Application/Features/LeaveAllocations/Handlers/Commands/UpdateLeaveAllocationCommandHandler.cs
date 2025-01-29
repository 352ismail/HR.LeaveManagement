using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Persistense.Contracts;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocatedRepository leaveAllocatedRepository;
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public UpdateLeaveAllocationCommandHandler(
            ILeaveAllocatedRepository leaveAllocatedRepository,
            IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveAllocatedRepository = leaveAllocatedRepository;
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocationValidation = new UpdateLeaveAllocationDTOValidator(leaveTypeRepository);
            var validationResult = await leaveAllocationValidation.ValidateAsync(request.UpdateLeaveAllocationDTO);
            if (!validationResult.IsValid)
            {
                throw new InvalidDataException();
            }
            var existingLeaveAllocation = await leaveAllocatedRepository.Get(request.UpdateLeaveAllocationDTO.Id);
            mapper.Map(request.UpdateLeaveAllocationDTO, existingLeaveAllocation);
            await leaveAllocatedRepository.Update(existingLeaveAllocation);
            return Unit.Value;
        }
    }
}
