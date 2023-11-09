using TaskManager.Application.Models;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Text.Json;
using TaskManager.Api.Tests.Helpers;

namespace TaskManager.Api.Tests.AccountTests
{
    public class RegistrationTests : TestBase
    {
        [Test]
        public async Task Ok_WhenAllNeededInfoSend()
        {
            var result = await HttpClient.Register(new RegistrationRequest()
            {
                Email = $"{Guid.NewGuid()}@gmail.com",
                FirstName = "User",
                Password = "Qwerty123@",
                LastName = "LastName",
                UserName = Guid.NewGuid().ToString()
            });

            var response = result.Success;
            response.Should().NotBeNull();
            response.UserId.Should().NotBeNull();
        }

        [Test]
        public async Task Register_DuplicateUsername_ThrowsException()
        {
            var username = "username";
            var result = await HttpClient.Register(new RegistrationRequest()
            {
                Email = "test1@gmail.com",
                FirstName = "User",
                Password = "Qwerty123@",
                LastName = "LastName",
                UserName = username
            });

            var errorMessage = result.Error;
            errorMessage.Should().Contain($"Username {username} already exists");
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Register_DuplicateEmail_ThrowsException()
        {
            var email = "test@gmail.com";
            var result = await HttpClient.Register(new RegistrationRequest()
            {
                Email = email,
                FirstName = "User",
                Password = "Qwerty123@",
                LastName = "LastName",
                UserName = "username1"
            });

            var errorMessage = result.Error;
            errorMessage.Should().Contain($"Email {email} already exists.");
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Register_WeakPassword_ThrowsException()
        {
            var result = await HttpClient.Register(new RegistrationRequest()
            {
                Email = $"{Guid.NewGuid()}@gmail.com",
                FirstName = "User",
                Password = "weak",
                LastName = "LastName",
                UserName = Guid.NewGuid().ToString()
            });

            var errorJson = result.Error;
            var errorObject = JsonSerializer.Deserialize<ErrorObject>(errorJson);

            errorObject.Should().NotBeNull();
            errorObject.Errors.Should().NotBeNull();
            errorObject.Errors.ContainsKey("Password").Should().BeTrue();
            errorObject.Errors["Password"].Should().Contain(error => error.Contains("password") || error.Contains("Password"));
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Register_InvalidEmailFormat_ThrowsException()
        {
            var result = await HttpClient.Register(new RegistrationRequest()
            {
                Email = "invalid_email",
                FirstName = "User",
                Password = "Qwerty123@",
                LastName = "LastName",
                UserName = Guid.NewGuid().ToString()
            });

            var errorJson = result.Error;
            var errorObject = JsonSerializer.Deserialize<ErrorObject>(errorJson);

            errorObject.Should().NotBeNull();
            errorObject.Errors.Should().NotBeNull();
            errorObject.Errors.ContainsKey("Email").Should().BeTrue();
            errorObject.Errors["Email"].Should().Contain(error => error.Contains("valid e-mail"));
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}