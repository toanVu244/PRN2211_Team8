using JPOS.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Repositories.Interfaces
{
    public interface IUserRepository: IGenericRepository<User>
    {
        public Task<User?> GetByUsernameAsync(string username);
        public Task<User?> GetUserByUsernameAndPasswordAsync(string username, string password);
        public User GetLastUserAsync();

        public User GetLastUserAsyncTest();

        public Task<User?> GetUserByEmail(string email);
    }

}
