using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using NUnit.Framework;
using Respawn;
using System.Net.Http.Headers;
using TaskManager.Api.Tests.Helpers;
using TaskManager.Application.DTOs;
using TaskManager.Application.Models;
using TaskManager.Domain;
using TaskManager.Infrastructure;
using static TaskManager.Api.Tests.Testing;

namespace TaskManager.Api.Tests
{
    public abstract class TestBase
    {
        protected const string DefaultUserEmail = "test@gmail.com";
        protected const string DefaultUserPassword = "Qwerty123@";

        protected TestBase()
        {
            HttpClient = new TaskManagerHttpClient(Client);
        }

        protected UserTaskDto GetDefaultUserTaskDto()
        {
            return new UserTaskDto()
            {
                Description = "Descriptions",
                Subject = "Subject",
                StartDateTime = DateTime.UtcNow,
                EndDateTime = DateTime.UtcNow.AddHours(5),
            };
        }

        [SetUp]
        public async Task TestSetup()
        {
            await SeedAndAuthenticate();
            var connectionString = Configuration.GetConnectionString("TaskManagerConnectionString");

            using (var conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();

                var respawner = await Respawner.CreateAsync(conn, new RespawnerOptions
                {
                    SchemasToInclude = new[]
                    {
                        "public"
                    },
                    DbAdapter = DbAdapter.Postgres
                });

                await respawner.ResetAsync(conn);
            }
        }

        public async Task SeedDefaultUser()
        {
            await HttpClient.Register(new RegistrationRequest()
            {
                Email = DefaultUserEmail,
                FirstName = "User",
                Password = DefaultUserPassword,
                LastName = "LastName",
                UserName = "username"
            });
        }

        public async Task SeedAndAuthenticate()
        {
            await SeedDefaultUser();

            var response = await HttpClient.Login(new AuthRequest()
            {
                Email = DefaultUserEmail,
                Password = DefaultUserPassword
            });

            HttpClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Success.Token);
        }

        public TaskManagerHttpClient HttpClient { get; private set; }

        public async Task AddUserTask(UserTask userTask)
        {
            using var scope = ScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TaskManagerDbContext>();

            dbContext.Add(userTask);
            await dbContext.SaveChangesAsync();
        }
    }
}