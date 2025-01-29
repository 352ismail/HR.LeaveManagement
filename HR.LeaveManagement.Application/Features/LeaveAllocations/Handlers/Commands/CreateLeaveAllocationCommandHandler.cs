using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Persistense.Contracts;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
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
        public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocationValidation = new CreateLeaveAllocationDTOValidator(leaveTypeRepository);
            var validationResult = await leaveAllocationValidation.ValidateAsync(request.CreateLeaveAllocationDTO);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }
            var leaveAllocation = mapper.Map<LeaveAllocation>(request);
            leaveAllocation = await leaveAllocatedRepository.Add(leaveAllocation);
            return leaveAllocation.Id;
        }
    }
}
