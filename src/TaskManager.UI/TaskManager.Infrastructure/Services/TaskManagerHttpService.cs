using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TaskManager.Infrastructure.Constants;
using TaskManager.Infrastructure.Contracts;
using TaskManager.Infrastructure.Dtos;

namespace TaskManager.Infrastructure.Services
{
    public class TaskManagerHttpService : ITaskManagerService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILocalStorageService localStorageService;
        private readonly ILogger<TaskManagerHttpService> logger;

        public TaskManagerHttpService(IHttpClientFactory httpClientFactory, ILogger<TaskManagerHttpService> logger, ILocalStorageService localStorageService)
        {
            this.httpClientFactory = httpClientFactory;
            this.localStorageService = localStorageService;
            this.logger = logger;
        }

        public async Task<int> Create(CreateUserTaskDto createDto, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = CreateHttpClient();
                var response = await httpClient.PostAsJsonAsync(ApiPaths.TaskManager.Root, createDto);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseValue = await response.Content.ReadFromJsonAsync<BaseCommandResponse>();
                    return responseValue.Id;
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error creating tasks");
                throw;
            }
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = CreateHttpClient();
                var response = await httpClient.DeleteAsync($"{ApiPaths.TaskManager.Root}/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error deleting task");
                throw;
            }
        }

        public async Task<bool> Edit(EditUserTaskDto editDto, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = CreateHttpClient();
                var response = await httpClient.PutAsJsonAsync(ApiPaths.TaskManager.Root, editDto);
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                { 
                    return response.IsSuccessStatusCode; 
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error retrieving tasks");
                throw;
            }
        }

        public async Task<List<UserTaskDto>> GetAllTasks(int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = CreateHttpClient();
                var allTasks = await httpClient.GetFromJsonAsync<List<UserTaskDto>>(ApiPaths.TaskManager.Root);
                allTasks = allTasks
                    .OrderBy(x => x.StartDateTime)
                    .ThenBy(x => x.StartDateTime.Hour)
                    .Skip((page - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .ToList();
                return allTasks;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error retrieving tasks");
                throw;
            }
        }

        public async Task<UserTaskDto> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = CreateHttpClient();
                var task = await httpClient.GetFromJsonAsync<UserTaskDto>($"{ApiPaths.TaskManager.Root}/{id}");
                return task;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error retrieving tasks");
                throw;
            }
        }

        public async Task<int> GetCount(CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = CreateHttpClient();
                var allTasks = await httpClient.GetFromJsonAsync<List<UserTaskDto>>(ApiPaths.TaskManager.Root);     
                return allTasks.Count;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error retrieving tasks");
                throw;
            }
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = this.httpClientFactory.CreateClient(ApiPaths.TaskManagerApiName);
            if (this.localStorageService.Exists("token"))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", this.localStorageService.GetStorageValue<string>("token"));
            }

            return httpClient;
        }
    }
}
