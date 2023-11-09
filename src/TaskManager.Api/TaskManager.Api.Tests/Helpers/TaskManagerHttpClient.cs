using System.Net.Http.Json;
using TaskManager.Application.DTOs;
using TaskManager.Application.Models;
using TaskManager.Application.Responses;

namespace TaskManager.Api.Tests.Helpers
{
    public class TaskManagerHttpClient
    {
        public TaskManagerHttpClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; private set; }

        public async Task<HttpClientResult<RegistrationResponse, string>> Register(RegistrationRequest request)
        {
            var response = await HttpClient.PostAsJsonAsync("account/register", request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<RegistrationResponse>();

                return HttpClientResult<RegistrationResponse, string>.CreateSuccess(responseContent, response.StatusCode);
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return HttpClientResult<RegistrationResponse, string>.CreateError(errorContent, response.StatusCode);
        }

        public async Task<HttpClientResult<AuthResponse, string>> Login(AuthRequest request)
        {
            var response = await HttpClient.PostAsJsonAsync("account/login", request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<AuthResponse>();

                return HttpClientResult<AuthResponse, string>.CreateSuccess(responseContent, response.StatusCode);
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return HttpClientResult<AuthResponse, string>.CreateError(errorContent, response.StatusCode);
        }

        public async Task<HttpClientResult<BaseCommandResponse, string>> Create(UserTaskDto request)
        {
            var response = await HttpClient.PostAsJsonAsync("tasks", request);
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<BaseCommandResponse>();

                return HttpClientResult<BaseCommandResponse, string>.CreateSuccess(responseContent, response.StatusCode);
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return HttpClientResult<BaseCommandResponse, string>.CreateError(errorContent, response.StatusCode);
        }

        public async Task<List<UserTaskDto>> GetAll()
        {
            var response = await HttpClient.GetAsync("tasks");
            var responseContent = await response.Content.ReadFromJsonAsync<List<UserTaskDto>>();

            return responseContent;
        }

        public async Task<bool> Edit(UserTaskDto request)
        {
            var response = await HttpClient.PutAsJsonAsync("tasks", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int id)
        {
            var response = await HttpClient.DeleteAsync($"tasks/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}