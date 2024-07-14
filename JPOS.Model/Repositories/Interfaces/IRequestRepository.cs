using JPOS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Repositories.Interfaces
{
    public interface IRequestRepository: IGenericRepository<Request>
    {        
        public Task<Request?> GetRequestByID(int requestID);
        public Task<Request?> GetLastRequestID();
        public Task<bool> UpdateStatusAsync(int requestId, string status);

        public Task<List<Request>> GetRequestByStatus(string status);

        public Task<List<Request>?> GetRequestByTime( int year,int month);

    }
}
