using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManager.Infrastructure.Contracts;
using TaskManager.Infrastructure.Dtos;
using TaskManager.Infrastructure.Services;
using TaskManager.UI.Models;
using TaskManager.UI.Services.Contracts;

namespace TaskManager.UI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService userService;
        private readonly ILocalStorageService localStorageService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private JwtSecurityTokenHandler tokenHandler;

        public AuthService(
            IUserService userService,
            ILocalStorageService localStorageService,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            this.userService = userService;
            this.localStorageService = localStorageService;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            tokenHandler = new JwtSecurityTokenHandler();
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                var token = await userService.Login(email, password);
                if (token != string.Empty)
                {
                    var tokenContent = tokenHandler.ReadJwtToken(token);
                    var claims = ParseClaims(tokenContent);
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                    await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                    localStorageService.SetStorageValue("token", token);

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AuthenticateWithGoogle(string email)
        {
            try
            {
                var token = await userService.GetUserByEmailAsync(email);

                if (token != null)
                {
                    var tokenContent = tokenHandler.ReadJwtToken(token);
                    var claims = ParseClaims(tokenContent);
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                    await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                    localStorageService.SetStorageValue("token", token);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Register(RegisterVM registration)
        {
            try
            {
                var registrationRequest = mapper.Map<RegisterDto>(registration);

                var userId = await userService.Register(registrationRequest);
                if (!string.IsNullOrEmpty(userId))
                {
                    await Authenticate(registration.Email, registration.Password);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Logout()
        {
            localStorageService.Clearstorage(new List<string> { "token" });
            await httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }
    }
}
