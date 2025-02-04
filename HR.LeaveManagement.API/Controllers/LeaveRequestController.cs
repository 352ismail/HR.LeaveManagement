using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.leaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator mediator;

        public LeaveRequestController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDTO>> Get(int id)
        {
            var leaveRequest = await mediator.Send(new GetLeaveRequestDetailRequest { Id = id });
            return Ok(leaveRequest);

        }
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestListDTO>>> Get()
        {
            var leaveRequest = await mediator.Send(new GetLeaveRequestListRequest());
            return Ok(leaveRequest);
        }
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse<LeaveRequestDTO>>> Post([FromBody] CreateLeaveRequestDTO createLeaveRequestDTO)
        {
            var leaveRequest = await mediator.Send(new CreateLeaveRequestCommand
            { CreateLeaveRequestDTO = createLeaveRequestDTO });
            return Ok(leaveRequest);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveRequestDTO updateLeaveRequestDTO)
        {
            var leaveRequest = mediator.Send(new UpdateLeaveRequestCommand
            { Id = id, UpdateLeaveRequestDTO = updateLeaveRequestDTO });
            return NoContent();
        }
        [HttpPut("ChangeApproval/{id}")]
        public async Task<ActionResult> changeApproval(int id, [FromBody] ChangeLeaveRequestApprovalDTO changeLeaveRequestApprovalDTO)
        {
            var leaveRequest = mediator.Send(new UpdateLeaveRequestCommand
            { Id = id, ChangeLeaveRequestApprovalDTO = changeLeaveRequestApprovalDTO });
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var leaveRequest = mediator.Send(new DeleteLeaveRequestCommand { Id = id });
            return NoContent();
        }
    }
}
