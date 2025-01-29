using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validator;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Persistense.Contracts;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    internal class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public UpdateLeaveRequestCommandHandler(
            ILeaveRequestRepository leaveRequestRepository,
            IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var existingLeaveRequest = await leaveRequestRepository.Get(request.Id);
            if (request.UpdateLeaveRequestDTO is not null)
            {
                var leaveAllocationValidation = new UpdateLeaveRequestDTOValidator(leaveTypeRepository);
                var validationResult = await leaveAllocationValidation.ValidateAsync(request.UpdateLeaveRequestDTO);
                if (!validationResult.IsValid)
                {
                    throw new InvalidDataException();
                }
                mapper.Map(request.UpdateLeaveRequestDTO, existingLeaveRequest);
                await leaveRequestRepository.Update(existingLeaveRequest);
            }
            else if (request.ChangeLeaveRequestApprovalDTO is not null)
            {
                mapper.Map(request.ChangeLeaveRequestApprovalDTO, existingLeaveRequest);
                await leaveRequestRepository.ChangeApprovalStatus(existingLeaveRequest, request.ChangeLeaveRequestApprovalDTO.Approved);
            }
            return Unit.Value;
        }
    }
}
