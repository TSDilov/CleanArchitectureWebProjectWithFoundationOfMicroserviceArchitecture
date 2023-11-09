using FluentAssertions;
using NUnit.Framework;
using TaskManager.Domain;

namespace TaskManager.Api.Tests.TasksTests
{
    public class GetAllTasks : TestBase
    {
        [Test]
        public async Task GetTasks()
        {
            var defaultTask = GetDefaultUserTaskDto();
            var result = await HttpClient.Create(defaultTask);
            var response = result.Success;
            var tasks = await HttpClient.GetAll();
            tasks.Should().HaveCount(1);
            tasks.FirstOrDefault(x => x.Id == response.Id).Should().NotBeNull();
        }

        [Test]
        public async Task GetTaskOnlyForCurrentUser()
        {
            var newTask = await AddTaskToDb();

            var defaultTask = GetDefaultUserTaskDto();
            var result = await HttpClient.Create(defaultTask);
            var response = result.Success;
            var tasks = await HttpClient.GetAll();
            tasks.Should().HaveCount(1);
            tasks.FirstOrDefault(x => x.Id == response.Id).Should().NotBeNull();
            tasks.FirstOrDefault(x => x.Id == newTask.Id).Should().BeNull();
        }

        private async Task<UserTask> AddTaskToDb()
        {
            var newTask = new Domain.UserTask()
            {
                Description = "Test",
                StartDateTime = DateTime.UtcNow,
                EndDateTime = DateTime.UtcNow,
                Subject = "Test",
                User = Guid.NewGuid().ToString()
            };
            await AddUserTask(newTask);
            return newTask;
        }
    }
}
