using JPOS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Repositories.Interfaces
{
    public interface IPolicyRepository : IGenericRepository<Policy>
    {
        public Task<Policy?> GetPolicyById(int id);
        public Task<List<Policy>?> GetAllPolicy();
        public Task<bool?> CreatePolicy(Policy policy);

        public Task<bool?> UpdatePolicy(int id, Policy policy);
        public Task<bool?> DeletePolicy(int id);
    }
}
