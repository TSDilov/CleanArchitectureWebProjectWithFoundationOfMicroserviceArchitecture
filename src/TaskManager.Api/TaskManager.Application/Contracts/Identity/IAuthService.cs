using TaskManager.Application.Models;

namespace TaskManager.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);

        Task<RegistrationResponse> Register(RegistrationRequest request);

        Task<AuthResponse> RegisterUserFromGoogleAsync(string email);

        Task<AuthResponse> GetUserByEmailAsync(string email);
    }
}
