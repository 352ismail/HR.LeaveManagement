using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        public Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? ApprovalStatus);
        public Task<List<LeaveRequestListDTO>> GetLeaveRequestWithDetails();
        public Task<LeaveRequestDTO> GetLeaveRequestWithDetails(int Id);
    }
}
