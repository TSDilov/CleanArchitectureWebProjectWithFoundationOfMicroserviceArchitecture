using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Contracts.Identity;
using TaskManager.Application.Models;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService authService;

        public AccountController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            try
            {
                var responce = await this.authService.Login(request);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            try
            {
                var response = await this.authService.Register(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var user = await authService.GetUserByEmailAsync(email);

                if (user != null)
                {
                    return Ok(user);
                }

                var registeredUser = await authService.RegisterUserFromGoogleAsync(email);

                if (registeredUser != null)
                {
                    return Ok(registeredUser);
                }

                return NotFound($"User with email {email} not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
