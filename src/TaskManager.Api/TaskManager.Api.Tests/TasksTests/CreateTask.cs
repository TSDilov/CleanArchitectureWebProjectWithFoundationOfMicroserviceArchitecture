using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Text.Json;
using TaskManager.Api.Tests.Helpers;
using TaskManager.Application.DTOs;

namespace TaskManager.Api.Tests.TasksTests
{
    public class CreateTask : TestBase
    {
        [Test]
        public async Task Ok_WhenAllNeededInfoSent()
        {
            var result = await HttpClient.Create(GetDefaultUserTaskDto());
            var response = result.Success;
            response.Should().NotBeNull();
            response.Id.Should().NotBe(0);
        }

        [Test]
        public async Task Create_WithoutSubject_ThrowsException()
        {
            var task = new UserTaskDto()
            {
                Description = "Descriptions",
                StartDateTime = DateTime.UtcNow,
                EndDateTime = DateTime.UtcNow.AddHours(1),
            };

            var result = await HttpClient.Create(task);
            var errorObject = JsonSerializer.Deserialize<ErrorObject>(result.Error);

            result.Should().NotBeNull();
            result.InSuccess().Should().BeFalse();
            errorObject.Should().NotBeNull();
            errorObject.Errors.Should().ContainKey("Subject");
            errorObject.Errors["Subject"].Should().Contain("The Subject field is required.");
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Create_WithoutDescription_ThrowsException()
        {
            var task = new UserTaskDto()
            {
                Subject = "Subject",
                StartDateTime = DateTime.UtcNow,
                EndDateTime = DateTime.UtcNow.AddHours(1),
            };

            var result = await HttpClient.Create(task);
            var errorObject = JsonSerializer.Deserialize<ErrorObject>(result.Error);

            result.Should().NotBeNull();
            result.InSuccess().Should().BeFalse();
            errorObject.Should().NotBeNull();
            errorObject.Errors.Should().ContainKey("Description");
            errorObject.Errors["Description"].Should().Contain("The Description field is required.");
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Create_StartDateLaterThenEndDate_ThrowsException()
        {
            var task = new UserTaskDto()
            {
                Subject = "Subject",
                Description = "Description",
                StartDateTime = DateTime.UtcNow.AddHours(1),
                EndDateTime = DateTime.UtcNow,
            };

            var result = await HttpClient.Create(task);
            var errorMessage = result.Error;

            result.Should().NotBeNull();
            result.InSuccess().Should().BeFalse();
            errorMessage.Should().NotBeNull();
            errorMessage.Should().Contain("Start date must be less than the end date");
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Create_StartDateEarlierThenDateNow_ThrowsException()
        {
            var task = new UserTaskDto()
            {
                Subject = "Subject",
                Description = "Description",
                StartDateTime = DateTime.UtcNow.AddHours(-1),
                EndDateTime = DateTime.UtcNow,
            };

            var result = await HttpClient.Create(task);
            var errorMessage = result.Error;

            result.Should().NotBeNull();
            result.InSuccess().Should().BeFalse();
            errorMessage.Should().NotBeNull();
            errorMessage.Should().Contain("Start date and time can not be earlier than the current date and time.");
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
