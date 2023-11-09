using TaskManager.UI.Models;

namespace TaskManager.UI.Services.Contracts
{
    public interface IAuthService
    {
        Task<bool> Authenticate(string email, string password);

        Task<bool> Register(RegisterVM registration);

        Task Logout();
    }
}
