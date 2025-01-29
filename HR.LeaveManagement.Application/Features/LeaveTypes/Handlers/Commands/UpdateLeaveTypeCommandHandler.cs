using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Persistense.Contracts;
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
            var validatorResult = await validator.ValidateAsync(request.UpdateLeaveTypeDTO);
            if (!validatorResult.IsValid)
            {
                throw new InvalidDataException();
            }
            var existingLeaveType = await leaveTypeRepository.Get(request.UpdateLeaveTypeDTO.Id);
            mapper.Map(request.UpdateLeaveTypeDTO, existingLeaveType);
            await leaveTypeRepository.Update(existingLeaveType);
            return Unit.Value;

        }
    }
}
