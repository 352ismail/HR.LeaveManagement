using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationController : ControllerBase
    {
        private readonly IMediator mediator;

        public LeaveAllocationController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseCommandResponse<LeaveAllocationDTO>>> Get(int id)
        {
            var leaveAllocation = await mediator.Send(new GetLeaveAllocationDetailRequest { Id = id });
            return Ok(leaveAllocation);
        }
        [HttpGet]
        public async Task<ActionResult<BaseCommandResponse<List<LeaveAllocationDTO>>>> Get()
        {
            var leaveAllocation = await mediator.Send(new GetLeaveAllocationListRequest());
            return Ok(leaveAllocation);
        }
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse<LeaveAllocationDTO>>> Post([FromBody] CreateLeaveAllocationDTO createLeaveAllocationDTO)
        {
            BaseCommandResponse<LeaveAllocationDTO>? leaveAllocation = await mediator.Send(new CreateLeaveAllocationCommand
            { CreateLeaveAllocationDTO = createLeaveAllocationDTO });
            return Ok(leaveAllocation);
        }
        [HttpPut]
        public async Task<ActionResult<BaseCommandResponse<LeaveAllocationDTO>>> Put([FromBody] UpdateLeaveAllocationDTO updateLeaveAllocationDTO)
        {
            var leaveAllocation = await mediator.Send(new UpdateLeaveAllocationCommand
            { UpdateLeaveAllocationDTO = updateLeaveAllocationDTO });
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseCommandResponse<string>>> Delete(int id)
        {
            var leaveAllocation = await mediator.Send(new DeleteLeaveAllocationCommand { Id = id });
            return NoContent();
        }
    }
}
