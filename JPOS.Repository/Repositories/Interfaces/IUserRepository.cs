﻿
using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Repository.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User?> GetByUsernameAsync(string username);
        public Task<User?> GetUserByUsernameAndPasswordAsync(string username, string password);
        public Task<User> GetLastUserAsync();

        public User GetLastUserAsyncTest();

        public Task<User?> GetUserByEmail(string email);
        Task<List<Role>> GetAllRolesAsync();

        Task<bool> HasRelatedRecordsAsync(string userId);
    }

}
