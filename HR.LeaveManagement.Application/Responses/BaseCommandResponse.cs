namespace HR.LeaveManagement.Application.Responses
{
    public class BaseCommandResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
