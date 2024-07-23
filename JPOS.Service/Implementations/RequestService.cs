﻿using JPOS.Model;
using BusinessObject.Entities;
using JPOS.Model.Models;
using JPOS.Model.Models.AppConfig;
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
    public class RequestService : IRequestService
    {
        /*private readonly IRequestRepository _requestrepository;

        public RequestService(IRequestRepository requestrepository)
        {
            _requestrepository = requestrepository;
        }*/

        public async Task<int> GenerateNextRequestIDAsync()
        {
            var lastRequest = await RequestRepository.Instance.GetLastRequestID();

            return lastRequest.Id;
        }

        public async Task<bool> CreateRequestAsync(Request request)
        {
            var result = await RequestRepository.Instance.AddRequestAsync(request);
            return result;
        }

        public async Task<bool> UpdateRequestAsync(Request request)
        {
            try
            {
                var result = await RequestRepository.Instance.UpdateAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteRequestAsync(int RequestID)
        {
            var result = await RequestRepository.Instance.DeleteAsync(RequestID);
            return (result);
        }

        public async Task<List<Request>> GetAllRequestAsync()
        {
            return await RequestRepository.Instance.GetAllAsync();
        }

        public async Task<Request> GetRequestByIDAsync(int RequestID)
        {
            return await RequestRepository.Instance.GetByIdAsync(RequestID);
        }

        public Task<bool> TrackingRequestAsync(int requestID)
        {
            throw new NotImplementedException();
        }




        public async Task<bool> ApproveRequestAsync(int requestId, string status)
        {
            var result = await RequestRepository.Instance.UpdateStatusAsync(requestId, status);          
            return result;
        }

        public async Task<List<Request>?> GetRequestByStatus(string? status, int role)
        {

            try
            {
                if (role != null)
                {
/*                    1: admin, 2 : manager, 3: sale, 4 : product, 5 : design, 6 : customer
*/
                    switch (role)
                    {
                        case 1:
                            {
                                return await RequestRepository.Instance.GetRequestByStatus(status); break;
                            }
                        case 2:
                            {
                                return await RequestRepository.Instance.GetRequestByStatus(status); break;
                            }
                        case 3:
                            {
                                if (status.Equals("Processing"))
                                {
                                    return await RequestRepository.Instance.GetRequestByStatus("Processing"); break;
                                }
                                if (status.Equals("Done"))
                                {
                                    return await RequestRepository.Instance.GetRequestByStatus("Done"); break;
                                }
                                if (status.Equals("All"))
                                {
                                    List<Request> a = await RequestRepository.Instance.GetRequestByStatus("Processing");
                                    a.AddRange(await RequestRepository.Instance.GetRequestByStatus("Done"));
                                    return a;

                                }
                                return null;
                            }
                        case 4:
                            {
                                return await RequestRepository.Instance.GetRequestByStatus("In-Production"); break;

                            }
                        case 5:
                            {
                                return await RequestRepository.Instance.GetRequestByStatus(""); break;

                            }
                        case 6:
                            {
                                return await RequestRepository.Instance.GetRequestByStatus(""); break;

                            }
                        default:
                            {
                                return await RequestRepository.Instance.GetRequestByStatus(status);

                            }
                    }


                }
                else
                {
                    return null;
                }



            }
            catch (Exception ex)
            {

                return null;
            }

        }

        //public async Task<List<StatisticRequest>?> GetRequestStatistic()
        //{
        //    int targetMonth = DateTime.Now.Month;
        //    int targetYear = DateTime.Now.Year;
        //    List<StatisticRequest> data = new List<StatisticRequest>();

        //    for (int i = 1; i <= targetMonth; i++)
        //    {
        //        var getAllinMonth = await _requestrepository.Requests.GetRequestByTime(targetYear, i);
        //        if (getAllinMonth.Count > 0)
        //        {
        //            StatisticRequest statisticRequest = new StatisticRequest();

        //            foreach (var b in getAllinMonth)
        //            {
        //                if (b.Type == 1)
        //                {
        //                    statisticRequest.OrderExist += 1;
        //                }
        //                if (b.Type == 2)
        //                {
        //                    statisticRequest.OrderCustome += 1;
        //                }
        //                if (b.Type == 3)
        //                {
        //                    statisticRequest.OrderDesign += 1;
        //                }
        //            }
        //            statisticRequest.Time = "Month " + i;
        //            data.Add(statisticRequest);
        //        }
        //    }
        //    return data;
        //}

        public async Task<List<StatisticRequest>> GetRequestStatistic()
        {
            int targetYear = DateTime.Now.Year;
            List<StatisticRequest> data = new List<StatisticRequest>();

            for (int i = 1; i <= 12; i++)
            {
                var getAllInMonth = await RequestRepository.Instance.GetRequestByTime(targetYear, i);
                StatisticRequest statisticRequest = new StatisticRequest
                {
                    Time = "Month " + i,
                    OrderExist = getAllInMonth.Count(r => r.Type == 1),
                    OrderCustome = getAllInMonth.Count(r => r.Type == 2),
                    OrderDesign = getAllInMonth.Count(r => r.Type == 3)
                };

                data.Add(statisticRequest);
            }
            return data;
        }

        public async Task<List<StatisticRequest>> GetRequestStatistic(int year)
        {
            int currentMonth = DateTime.Now.Month;
            int targetYear = DateTime.Now.Year;
            List<StatisticRequest> data = new List<StatisticRequest>();

            for (int i = 1; i <= (year == targetYear ? currentMonth : 12); i++)
            {
                var getAllInMonth = await RequestRepository.Instance.GetRequestByTime(year, i);
                StatisticRequest statisticRequest = new StatisticRequest
                {
                    Time = "Month " + i,
                    OrderExist = getAllInMonth.Count(r => r.Type == 1),
                    OrderCustome = getAllInMonth.Count(r => r.Type == 2),
                    OrderDesign = getAllInMonth.Count(r => r.Type == 3)
                };

                data.Add(statisticRequest);
            }
            return data;
        }




        public Task<Request> GetLastRequest()
        {
            return RequestRepository.Instance.GetLastRequestID();
        }

        public async Task<List<Request>> GetRequestsByUserIdAsync(string userId)
        {
            return await RequestRepository.Instance.GetRequestsByUserIdAsync(userId);
        }

        public Task<bool> CancelRequestAsync(int requestId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool?> DeatachRequest(Request model)
        {
            if (model != null)
            {
                RequestRepository.Instance.Detach(model);
                return true;
            }
            return false;
        }
    }
}
