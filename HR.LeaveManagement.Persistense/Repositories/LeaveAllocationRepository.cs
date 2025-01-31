using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence;

namespace HR.LeaveManagement.Persistense.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocatedRepository
    {
        private readonly LeaveManagementDBContext context;

        public LeaveAllocationRepository(LeaveManagementDBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
