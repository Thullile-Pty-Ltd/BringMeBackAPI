﻿using BringMeBackAPI.Models.Users;

namespace BringMeBackAPI.Services.Users.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);

        Task<User> RegisterAsync(User user);
        Task<User> AuthenticateAsync(string email, string password);
    }
}
