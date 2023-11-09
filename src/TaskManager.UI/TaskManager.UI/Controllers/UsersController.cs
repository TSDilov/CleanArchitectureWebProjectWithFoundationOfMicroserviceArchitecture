using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Infrastructure.Dtos;
using TaskManager.UI.Models;
using TaskManager.UI.Services.Contracts;

namespace TaskManager.UI.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAuthService authService;
        private readonly IMapper mapper;

        public UsersController(IAuthService authService, IMapper mapper)
        {
            this.authService = authService;
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

