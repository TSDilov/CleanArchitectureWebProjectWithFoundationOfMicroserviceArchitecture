﻿using TaskManager.Infrastructure.Dtos;

namespace TaskManager.Infrastructure.Contracts
{
    public interface IUserService
    {
        Task<string> Login(string email, string password);

        Task<string> GetUserByEmailAsync(string email);

        Task<string> Register(RegisterDto registration);
    }
}
