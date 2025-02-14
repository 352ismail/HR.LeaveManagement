using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    internal class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveTypeDTOValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateLeaveTypeDTO);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }
            var leaveType = await leaveTypeRepository.Get(request.UpdateLeaveTypeDTO.Id);
            if (leaveType is null)
            {
                throw new NotFoundException(nameof(leaveType), request.UpdateLeaveTypeDTO.Id);
            }
            mapper.Map(request.UpdateLeaveTypeDTO, leaveType);
            await leaveTypeRepository.Update(leaveType);
            return Unit.Value;

        }
    }
}
