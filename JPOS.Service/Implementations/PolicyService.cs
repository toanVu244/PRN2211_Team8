using AutoMapper;
using JPOS.Model;
using BusinessObject.Entities;
using JPOS.Model.Models;
using JPOS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPOS.Repository.Repositories.Interfaces;

namespace JPOS.Service.Implementations
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _policyrepository;
        private readonly IMapper _mapper;

        public PolicyService(IPolicyRepository policyrepository, IMapper mapper)
        {
            _policyrepository = policyrepository;
            _mapper = mapper;
        }

        public async Task<bool?> CreatePolicy(PolicyModel policy)
        {
            if (policy == null)
            {
                return false;
            }
            return await _policyrepository.CreatePolicy(_mapper.Map<Policy>(policy));
        }

        public Task<bool?> DeletePolicy(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Policy>?> GetAllPolicy()
        {
            return await _policyrepository.GetAllPolicy();
        }

        public async Task<Policy?> GetPolicyById(int id)
        {
            return await _policyrepository.GetById(id);
        }

        public async Task<bool?> UpdatePolicy(int id, PolicyModel policy)
        {
            if (policy == null || id == null)
            {

                return false;
            }
         return await _policyrepository.UpdateAsync(_mapper.Map<Policy>(policy));


        }
    }
}
