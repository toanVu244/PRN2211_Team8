using JPOS.Model.Entities;
using JPOS.Model.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Repositories.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly JPOS_ProjectContext _context;
        public UserRepository(JPOS_ProjectContext context) : base(context)
        {
            _context = context;
        }
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }
        public async Task<User?> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public  User GetLastUserAsync()
        {
            /*var lastUser = await _context.Users
                .OrderByDescending(u => u.UserId)
                .FirstOrDefaultAsync();*/

            return _context.Users.OrderByDescending(u => u.UserId).FirstOrDefault();  /*await _context.Users.OrderByDescending(u => u.UserId).FirstOrDefaultAsync();*/
        }

        public async Task<User?> GetUserByEmail(string email)
        {
           return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
            
        public User GetLastUserAsyncTest()
        {
            return _context.Users.OrderByDescending(u => u.UserId).FirstOrDefault();
        }
    }
}
