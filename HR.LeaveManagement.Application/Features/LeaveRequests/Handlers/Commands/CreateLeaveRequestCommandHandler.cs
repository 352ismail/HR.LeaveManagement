using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validator;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse<LeaveRequestDTO>>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public CreateLeaveRequestCommandHandler(
            ILeaveRequestRepository leaveRequestRepository,
            IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<BaseCommandResponse<LeaveRequestDTO>> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocationValidation = new CreateLeaveRequestDTOValidator(leaveTypeRepository);
            var validationResult = await leaveAllocationValidation.ValidateAsync(request.CreateLeaveRequestDTO);
            if (!validationResult.IsValid)
            {
                return new BaseCommandResponse<LeaveRequestDTO>()
                {
                    Success = false,
                    Message = "Creation Failed",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var leaveRequest = this.mapper.Map<LeaveRequest>(request.CreateLeaveRequestDTO);
            leaveRequest = await leaveRequestRepository.Add(leaveRequest);
            return new BaseCommandResponse<LeaveRequestDTO>()
            {
                Success = true,
                Message = "Creation Successful",
                Data = mapper.Map<LeaveRequestDTO>(leaveRequest)
            };
        }
    }
}
