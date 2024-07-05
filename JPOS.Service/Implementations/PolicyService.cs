using AutoMapper;
using JPOS.Model;
using JPOS.Model.Entities;
using JPOS.Model.Models;
using JPOS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Implementations
{
    public class PolicyService : IPolicyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PolicyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool?> CreatePolicy(PolicyModel policy)
        {
           if (policy == null) {
                return false;
            }
          return await _unitOfWork.Policies.CreatePolicy(_mapper.Map<Policy>(policy));
        }

        public Task<bool?> DeletePolicy(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Policy>?> GetAllPolicy()
        {
           return await _unitOfWork.Policies.GetAllPolicy();
        }

        public async Task<Policy?> GetPolicyById(int id)
        {
           return await _unitOfWork.Policies.GetById(id);
        }

        public async Task<bool?> UpdatePolicy(int id, PolicyModel policy)
        {
           if(policy == null || id == null) {

                return false;
                    }
            await _unitOfWork.Policies.UpdateAsync(_mapper.Map<Policy>(policy));
           
             return await _unitOfWork.CompleteAsync() >=0;
           
        }
    }
}
