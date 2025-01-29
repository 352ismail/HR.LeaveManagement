using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Persistense.Contracts;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
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
        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveTypeDTOValidator();
            var validatorResult = await validator.ValidateAsync(request.CreateLeaveTypeDTO);
            if (!validatorResult.IsValid)
            {
                throw new InvalidDataException();
            }
            var leaveType = mapper.Map<LeaveType>(request.CreateLeaveTypeDTO);
            leaveType = await leaveTypeRepository.Add(leaveType);
            return leaveType.Id;
        }
    }
}
