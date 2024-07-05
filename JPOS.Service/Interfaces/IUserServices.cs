using JPOS.Model.Entities;
using JPOS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Interfaces
{
    public interface IUserServices
    {
        public Task<string> AuthenticateAsync(string username, string password);
        public Task<List<User>> GetAllUsersAsync();
        public Task<User?> GetUserByIdAsync(string id);
        public Task<bool> CreateUserAsync(User user);
        public Task<User?> GetUserByUsernameAsync(string username);
        public Task<bool> UpdateUserAsync(User user);
        public Task<bool> DeleteUserAsync(string id);
        public Task<bool> UserRegister(RegisterModel model);
        public Task<User?> GetUserByEmail(string email);
        public string HashAndTruncatePassword(string password);
    }
}
