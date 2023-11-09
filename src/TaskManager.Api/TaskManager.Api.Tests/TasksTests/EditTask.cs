using FluentAssertions;
using NUnit.Framework;

namespace TaskManager.Api.Tests.TasksTests
{
    public class EditTask : TestBase
    {
        [Test]
        public async Task Ok_WhenAllNeededInfoSent()
        {
            var defaultTask = GetDefaultUserTaskDto();
            var result = await HttpClient.Create(defaultTask);
            var response = result.Success;
            defaultTask.Id = response.Id;
            defaultTask.Description = "ChangedDescription";

            var isSuccess = await HttpClient.Edit(defaultTask);

            isSuccess.Should().BeTrue();
        }
    }
}
