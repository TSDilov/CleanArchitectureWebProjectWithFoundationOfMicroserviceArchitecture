using FluentAssertions;
using NUnit.Framework;

namespace TaskManager.Api.Tests.TasksTests
{
    public class DeleteTask : TestBase
    {
        [Test]
        public async Task Ok_WhenAllNeededInfoSent()
        {
            var defaultTask = GetDefaultUserTaskDto();
            var result = await HttpClient.Create(defaultTask);
            var response = result.Success;
            await HttpClient.Delete(response.Id);
            var tasks = await HttpClient.GetAll();

            tasks.FirstOrDefault(x => x.Id == response.Id).Should().BeNull();
        }
    }
}
