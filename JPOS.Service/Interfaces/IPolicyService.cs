using BusinessObject.Entities;
using JPOS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Interfaces
{
    public interface IPolicyService
    {

        public Task<Policy?> GetPolicyById(int id);
        public Task<List<Policy>?> GetAllPolicy();
        public Task<bool?> CreatePolicy(PolicyModel policy);

        public Task<bool?> UpdatePolicy(int id, PolicyModel policy);
        public Task<bool?> DeletePolicy(int id);
    }
}
