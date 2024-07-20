﻿using BusinessObject.Entities;
using JPOS.Model.Models;

namespace JPOS.Service.Interfaces
{
    public interface IUserServices
    {
        public Task<User> AuthenticateAsync(string username, string password);
        public Task<List<User>> GetAllUsersAsync();
        public Task<User?> GetUserByIdAsync(string id);
        public Task<bool> CreateUserAsync(User user);
        public Task<User?> GetUserByUsernameAsync(string username);
        public Task<bool> UpdateUserAsync(User user);
        public Task<bool> DeleteUserAsync(string id);
        public Task<bool> UserRegister(RegisterModel model);
        public Task<User?> GetUserByEmail(string email);
        public string HashAndTruncatePassword(string password);
        Task<List<Role>> GetAllRolesAsync();
        public void sendmail(string mail, string body);

        public string GenerateRandomOTP();

        public Task<bool> ConfirmEmail(string email);

        public Task<bool> ResetPassword(string email, string password, string otp);

        Task<bool> HasRelatedRecordsAsync(string userId);
    }
}