using Microsoft.AspNetCore.Http;
using TaskManager.Application.Constants;
using TaskManager.Application.Contracts.Identity;

namespace TaskManager.Infrastructure.Services
{
    public class GetCurrentUserService : ICurrentUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public GetCurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUser()
        {
            var claim = this.httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid);

            return claim?.Value;
        }
    }
}
