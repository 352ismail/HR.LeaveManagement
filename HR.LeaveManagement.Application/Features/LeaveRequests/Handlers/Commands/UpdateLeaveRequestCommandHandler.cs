using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validator;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    internal class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, BaseCommandResponse<LeaveRequestDTO>>
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
        public async Task<BaseCommandResponse<LeaveRequestDTO>> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.Get(request.Id);
            if (leaveRequest is null)
            {
                return new BaseCommandResponse<LeaveRequestDTO>()
                {
                    Success = false,
                    Message = "Record Not found",
                };
            }
            if (request.UpdateLeaveRequestDTO is not null)
            {
                var leaveAllocationValidation = new UpdateLeaveRequestDTOValidator(leaveTypeRepository);
                var validationResult = await leaveAllocationValidation.ValidateAsync(request.UpdateLeaveRequestDTO);
                if (!validationResult.IsValid)
                {
                    return new BaseCommandResponse<LeaveRequestDTO>()
                    {
                        Success = false,
                        Message = "Update Failed",
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                    };
                }
                mapper.Map(request.UpdateLeaveRequestDTO, leaveRequest);
                await leaveRequestRepository.Update(leaveRequest);
            }
            else if (request.ChangeLeaveRequestApprovalDTO is not null)
            {
                mapper.Map(request.ChangeLeaveRequestApprovalDTO, leaveRequest);
                await leaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDTO.Approved);
            }
            return new BaseCommandResponse<LeaveRequestDTO>()
            {
                Success = true,
                Message = "Success",
                Data = mapper.Map<LeaveRequestDTO>(leaveRequest)
            };
        }
    }
}
