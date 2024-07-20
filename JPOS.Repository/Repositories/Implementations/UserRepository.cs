﻿
using BusinessObject.Entities;
using JPOS.DAO.EntitiesDAO;
using JPOS.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Repository.Repositories.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private static UserRepository _instance;

        public static UserRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserRepository();
                }
                return _instance;
            }
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }
        public async Task<User?> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<User> GetLastUserAsync()
        {
            var lastUser = await _context.Users
                .OrderByDescending(u => u.UserId)
                .FirstOrDefaultAsync();
            return _context.Users.OrderByDescending(u => u.UserId).FirstOrDefault();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public User GetLastUserAsyncTest()
        {
            return _context.Users.OrderByDescending(u => u.UserId).FirstOrDefault();
        }

        public async Task<bool> HasRelatedRecordsAsync(string userId)
        {
            var hasBlogs = await _context.Blogs.AnyAsync(b => b.CreateBy == userId);
            var hasPolicies = await _context.Policies.AnyAsync(p => p.CreateBy == userId);
            var hasRequests = await _context.Requests.AnyAsync(r => r.UserId == userId);
            var hasFeedbacks = await _context.Feedbacks.AnyAsync(f => f.UserId == userId);
            var hasTransactions = await _context.Transactions.AnyAsync(t => t.UserId == userId);

            return hasBlogs || hasPolicies || hasRequests || hasFeedbacks || hasTransactions;
        }
    }
}
