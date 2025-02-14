using HR.LeaveManagement.Application.Model;

namespace HR.LeaveManagement.Application.Contracts.Infrastructure
{
    public interface IEmailSender
    {
        public Task<bool> SendEmail(Email email);
    }
}
