using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validator;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Model;
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
        private readonly IEmailSender emailSender;

        public CreateLeaveRequestCommandHandler(
            ILeaveRequestRepository leaveRequestRepository,
            IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository,
            IEmailSender emailSender)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
            this.emailSender = emailSender;
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

            try
            {
                var result = await emailSender.SendEmail(new Email
                {
                    To = "352ismailkhan@gmail.com",
                    Subject = "Leave Request Submitted",
                    Body = $"Your leave request for {request.CreateLeaveRequestDTO.StartDate:D} to {request.CreateLeaveRequestDTO.EndDate:D}"
                });
            }
            catch
            {
                //log exceptions
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
