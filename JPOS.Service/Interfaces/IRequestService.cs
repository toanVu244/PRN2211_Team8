using JPOS.Model.Entities;
using JPOS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Interfaces
{
    public interface IRequestService
    {
        public Task<List<Request>> GetAllRequestAsync();
        public Task<Request> GetRequestByIDAsync(int RequestID);
        public Task<Request> GetLastRequest();
        public Task<bool> CreateRequestAsync(Request request);
        public Task<bool> UpdateRequestAsync(Request request);
        public Task<bool> DeleteRequestAsync(int RequestID);
        public Task<bool> CancelRequestAsync(int requestId);
        public Task<bool> TrackingRequestAsync(int requestID);
        Task<bool> ApproveRequestAsync(int requestId, string status);

        public Task<List<Request>?> GetRequestByStatus(string? status, int role);

        public Task<List<StatisticRequest>?> GetRequestStatistic();

    }
}
