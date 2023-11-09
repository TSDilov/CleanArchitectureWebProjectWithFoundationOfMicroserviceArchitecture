using FluentAssertions;
using NUnit.Framework;
using System.Net;
using TaskManager.Application.Models;

namespace TaskManager.Api.Tests.AccountTests
{
    public class LoginTests : TestBase
    {
        [Test]
        public async Task Ok_WhenAllNeededInfoSend()
        {
            var result = await HttpClient.Login(new AuthRequest()
            {
                Email = DefaultUserEmail,
                Password = DefaultUserPassword
            });

            var response = result.Success;
            response.Should().NotBeNull();
            response.Token.Should().NotBeNull();
        }

        [Test]
        public async Task Login_EmailNotFound_ThrowsException()
        {
            var loginRequest = new AuthRequest()
            {
                Email = "test1@gmail.com",
                Password = DefaultUserPassword
            };

            var result = await HttpClient.Login(loginRequest);
            var errorMessage = result.Error;
            errorMessage.Should().Contain($"User with {loginRequest.Email} not found.");
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Login_InvalidPassword_ThrowsException()
        {
            var loginRequest = new AuthRequest()
            {
                Email = DefaultUserEmail,
                Password = "wrongpass123@"
            };

            var result = await HttpClient.Login(loginRequest);
            var errorMessage = result.Error;
            errorMessage.Should().Contain($"Credentials for {loginRequest.Email} aren't valid");
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
