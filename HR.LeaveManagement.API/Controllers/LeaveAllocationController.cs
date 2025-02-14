using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
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
        public async Task<ActionResult<GetLeaveAllocationDetailRequest>> Get(int id)
        {
            var leaveAllocation = await mediator.Send(new GetLeaveAllocationDetailRequest { Id = id });
            return Ok(leaveAllocation);
        }
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDTO>>> Get()
        {
            var leaveAllocation = await mediator.Send(new GetLeaveAllocationListRequest());
            return Ok(leaveAllocation);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationDTO createLeaveAllocationDTO)
        {
            var leaveAllocation = await mediator.Send(new CreateLeaveAllocationCommand
            { CreateLeaveAllocationDTO = createLeaveAllocationDTO });
            return Ok(leaveAllocation);
        }
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationDTO updateLeaveAllocationDTO)
        {
            var leaveAllocation = mediator.Send(new UpdateLeaveAllocationCommand
            { UpdateLeaveAllocationDTO = updateLeaveAllocationDTO });
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var leaveAllocation = mediator.Send(new DeleteLeaveAllocationCommand { Id = id });
            return NoContent();
        }
    }
}
