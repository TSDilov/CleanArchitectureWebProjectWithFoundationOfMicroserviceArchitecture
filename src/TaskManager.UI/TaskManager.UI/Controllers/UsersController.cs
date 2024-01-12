using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using TaskManager.Infrastructure.Dtos;
using TaskManager.UI.Models;
using TaskManager.UI.Services.Contracts;

namespace TaskManager.UI.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAuthService authService;
        private readonly IOptions<GoogleAuthSettings> googleAuthSettings;
        private readonly IMapper mapper;

        public UsersController(IAuthService authService, IOptions<GoogleAuthSettings> googleAuthSettings, IMapper mapper)
        {
            this.authService = authService;
            this.googleAuthSettings = googleAuthSettings;
            this.mapper = mapper;
        }

        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var returnUrl = Url.Content("~/");
                var isLoggedIn = await authService.Authenticate(login.Email, login.Password);
                if (isLoggedIn)
                {
                    return LocalRedirect(returnUrl);
                }
            }

            ModelState.AddModelError("", "Log In Attempt Failed. Please try again.");
            return View(login);
        }

        public async Task ExternalLogin(string returnUrl = null)
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleCallback")
                }); 
        }

        [HttpGet]
        public async Task<IActionResult> GoogleCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
            {
                return RedirectToAction("Login");
            }

            var email = authenticateResult.Principal.FindFirst(ClaimTypes.Email)?.Value;

            var isLoggedIn = await authService.AuthenticateWithGoogle(email);

            if (isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registration)
        {
            try
            {
                var returnUrl = Url.Content("~/");
                if (ModelState.IsValid)
                {
                    var dto = mapper.Map<RegisterDto>(registration);
                    await authService.Register(registration);
                }

                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(registration);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            await authService.Logout();
            return LocalRedirect(returnUrl);
        }
    }
}

