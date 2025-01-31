using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence;

namespace HR.LeaveManagement.Persistense.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        private readonly LeaveManagementDBContext context;

        public LeaveTypeRepository(LeaveManagementDBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
