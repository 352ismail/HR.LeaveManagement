using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly IMediator mediator;

        public LeaveTypeController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveTypeDTO>> Get(int id)
        {
            var leaveType = await mediator.Send(new GetLeaveTypeDetailRequest { Id = id });
            return Ok(leaveType);
        }
        [HttpGet]
        public async Task<ActionResult<List<LeaveTypeDTO>>> Get()
        {
            var leaveType = await mediator.Send(new GetLeaveTypeListRequest());
            return Ok(leaveType);
        }
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse<LeaveTypeDTO>>> Post([FromBody] CreateLeaveTypeDTO createLeaveTypeDTO)
        {
            var leaveType = await mediator.Send(new CreateLeaveTypeCommand
            { CreateLeaveTypeDTO = createLeaveTypeDTO });
            return Ok(leaveType);
        }
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveTypeDTO updateLeaveTypeDTO)
        {
            var leaveType = mediator.Send(new UpdateLeaveTypeCommand
            { UpdateLeaveTypeDTO = updateLeaveTypeDTO });
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var leaveType = mediator.Send(new DeleteLeaveTypeCommand { Id = id });
            return NoContent();
        }
    }
}
