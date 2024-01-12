using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using TaskManager.Infrastructure.Constants;
using TaskManager.Infrastructure.Contracts;
using TaskManager.Infrastructure.Dtos;

namespace TaskManager.Infrastructure.Services
{
    public class UserHttpService : IUserService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<UserHttpService> logger;

        public UserHttpService(IHttpClientFactory httpClientFactory, ILogger<UserHttpService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<string> Login(string email, string password)
        {
            try
            {
                var httpClient = this.httpClientFactory.CreateClient(ApiPaths.TaskManagerApiName);
                var loginResponse = await httpClient.PostAsJsonAsync(ApiPaths.UserManager.Login, new LoginDto()
                {
                    Email = email,
                    Password = password
                });

                var response = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDto>();

                return response.Token;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error loging user");
                throw;
            }
        }

        public async Task<string> GetUserByEmailAsync(string email)
        {
            try
            {
                var httpClient = this.httpClientFactory.CreateClient(ApiPaths.TaskManagerApiName);
                var loginResponse = await httpClient.GetAsync($"{ApiPaths.UserManager.GetUserByEmail}?email={email}");
                if (loginResponse.IsSuccessStatusCode)
                {
                    var response = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDto>();
                    return response.Token;
                }

                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error retrieving user by email");
                throw;
            }
        }

        public async Task<string> Register(RegisterDto registration)
        {
            try
            {
                var httpClient = this.httpClientFactory.CreateClient(ApiPaths.TaskManagerApiName);
                var registerResponse = await httpClient.PostAsJsonAsync(ApiPaths.UserManager.Register, registration);

                if (!registerResponse.IsSuccessStatusCode)
                {
                    throw new Exception(await registerResponse.Content.ReadAsStringAsync());
                }

                var response = await registerResponse.Content.ReadFromJsonAsync<RegisterResponseDto>();

                return response.UserId;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error registering the user");
                throw;
            }
        }
    }
}
