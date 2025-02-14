using HR.LeaveManagement.MVC.Contracts;

namespace HR.LeaveManagement.MVC.Services.Base
{
    public class BaseHttpService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IClient client;

        public BaseHttpService(ILocalStorageService localStorageService, IClient client)
        {
            this.localStorageService = localStorageService;
            this.client = client;
        }

        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
        {
            if (ex.StatusCode == 400)
            {
                return new Response<Guid>()
                {
                    ValidationErrors = ex.Message,
                    Message = "Validation error occured",
                    Success = false
                };
            }
            else if (ex.StatusCode == 404)
            {
                return new Response<Guid>()
                {
                    Message = "The requested item could not be found",
                    Success = false
                };
            }
            else
            {
                return new Response<Guid>()
                {
                    Message = "Something went wrong, please try again",
                    Success = false
                };
            }
        }

        protected void AddBearerToken()
        {
            if (localStorageService.Exists("token"))
            {
                client.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", localStorageService.GetStorageValue<string>("token"));
            }
        }

    }
}
