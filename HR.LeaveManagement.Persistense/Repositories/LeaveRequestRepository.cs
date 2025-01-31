using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistense.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly LeaveManagementDBContext context;
        private readonly IMapper mapper;

        public LeaveRequestRepository(LeaveManagementDBContext context, IMapper mapper) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? ApprovalStatus)
        {
            leaveRequest.Approved = ApprovalStatus;
            context.Entry(leaveRequest).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<List<LeaveRequestListDTO>> GetLeaveRequestWithDetails()
        {
            var leaveRequests = await context.LeaveRequest
                .Include(x => x.LeaveType)
                .ToListAsync();
            return mapper.Map<List<LeaveRequestListDTO>>(leaveRequests);
        }

        public async Task<LeaveRequestDTO> GetLeaveRequestWithDetails(int Id)
        {
            var leaveRequests = await context.LeaveRequest
                .Include(x => x.LeaveType)
                .FirstOrDefaultAsync(x => x.Id.Equals(Id));
            return mapper.Map<LeaveRequestDTO>(leaveRequests);
        }
    }
}
