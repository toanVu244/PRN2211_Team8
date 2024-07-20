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
using JPOS.Repository.Repositories.Implementations;

namespace JPOS.Service.Implementations
{
    public class PolicyService : IPolicyService
    {
        private readonly IMapper _mapper;

        public PolicyService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<bool?> CreatePolicy(PolicyModel policy)
        {
            if (policy == null)
            {
                return false;
            }
            return await PolicyRepository.Instance.CreatePolicy(_mapper.Map<Policy>(policy));
        }

        public Task<bool?> DeletePolicy(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Policy>?> GetAllPolicy()
        {
            return await PolicyRepository.Instance.GetAllPolicy();
        }

        public async Task<Policy?> GetPolicyById(int id)
        {
            return await PolicyRepository.Instance.GetById(id);
        }

        public async Task<bool?> UpdatePolicy(int id, PolicyModel policy)
        {
            if (policy == null || id == null)
            {

                return false;
            }
         return await PolicyRepository.Instance.UpdateAsync(_mapper.Map<Policy>(policy));


        }
    }
}
