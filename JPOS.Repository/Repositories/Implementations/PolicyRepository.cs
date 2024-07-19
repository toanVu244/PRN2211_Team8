
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
    public class PolicyRepository : GenericRepository<Policy>, IPolicyRepository
    {
        private readonly JPOS_DatabaseContext _context;

        public PolicyRepository(JPOS_DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool?> CreatePolicy(Policy policy)
        {
            await _context.Policies.AddAsync(policy);
            return _context.SaveChanges() > 0;
        }

        public Task<bool?> DeletePolicy(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Policy>?> GetAllPolicy()
        {
            return await _context.Policies.ToListAsync();
        }

        public async Task<Policy?> GetPolicyById(int id)
        {
            return await _context.Policies.FirstOrDefaultAsync(x => x.PolicyId == id);
        }

        public async Task<bool?> UpdatePolicy(int id, Policy policy)
        {
            var oldPolicy = await _context.Policies.FirstOrDefaultAsync(x => x.PolicyId == id);
            if (oldPolicy != null)
            {
                oldPolicy.PolicyId = id;
                oldPolicy.Title = policy.Title;
                oldPolicy.CreateDate = DateTime.Now;
                oldPolicy.Content = policy.Content;
                _context.Policies.Update(policy);
                return _context.SaveChanges() > 0;
            }
            return false;
        }
    }
}
