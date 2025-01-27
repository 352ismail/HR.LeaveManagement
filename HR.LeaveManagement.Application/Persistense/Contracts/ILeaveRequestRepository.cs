using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Persistense.Contracts
{
    public interface ILeaveRequestRepository : IGenericRespository<LeaveRequest>
    {
        public Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? ApprovalStatus);
        public Task<List<LeaveRequestListDTO>> GetLeaveRequestWithDetails();
        public Task<LeaveRequestDTO> GetLeaveRequestWithDetails(int Id);
    }
}
