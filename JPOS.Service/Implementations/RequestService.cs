using JPOS.Model;
using JPOS.Model.Entities;
using JPOS.Model.Models;
using JPOS.Model.Models.AppConfig;
using JPOS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Implementations
{
    public class RequestService: IRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> GenerateNextRequestIDAsync()
        {
            var lastRequest = await _unitOfWork.Requests.GetLastRequestID();

            return lastRequest.Id;
        }

        public async Task<bool> CreateRequestAsync(Request request)
        {
            /*int nextRequestID = await GenerateNextRequestIDAsync();
            request.ID = nextRequestID + 1;*/

            var result = await _unitOfWork.Requests.InsertAsync(request);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> UpdateRequestAsync(Request request)
        {
            var result = await _unitOfWork.Requests.UpdateAsync(request);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> DeleteRequestAsync(int RequestID)
        {
            var result = await _unitOfWork.Requests.DeleteAsync(RequestID); 
            await _unitOfWork.CompleteAsync();
            return (result);
        }

        public async Task<List<Request>> GetAllRequestAsync()
        {
            return await _unitOfWork.Requests.GetAllAsync();
        }

        public async Task<Request> GetRequestByIDAsync(int RequestID)
        {
            return await _unitOfWork.Requests.GetByIdAsync(RequestID);
        }

        public Task<bool> TrackingRequestAsync(int requestID)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> CancelRequestAsync(int requestId)
        {
            var request = await _unitOfWork.Requests.GetByIdAsync(requestId);
            request.Status = "Canceled";
            var result = await _unitOfWork.Requests.UpdateAsync(request);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> ApproveRequestAsync(int requestId, string status)
        {
            var result = await _unitOfWork.Requests.UpdateStatusAsync(requestId, status);
            if (result)
            {
                await _unitOfWork.CompleteAsync();
            }
            return result;
        }

        public async Task<List<Request>?> GetRequestByStatus(string? status, int role)
        {
           
            try { 
                if(role !=null)
                {
                 /*  1: admin, 2 : manager, 3: sale, 4 : product, 5 : design, 6 : customer */

                    switch (role)
                    {
                        case 1:
                            {
                                return await _unitOfWork.Requests.GetRequestByStatus(status); break;
                            }
                        case 2:
                            {
                                return await _unitOfWork.Requests.GetRequestByStatus(status); break;
                            }
                        case 3:
                            {                           
                                if (status.Equals("Processing"))
                                {
                                    return await _unitOfWork.Requests.GetRequestByStatus("Processing"); break;
                                }
                                if (status.Equals("Done"))
                                {
                                    return await _unitOfWork.Requests.GetRequestByStatus("Done"); break;
                                }
                                if (status.Equals("All"))
                                {
                                    List<Request> a = await _unitOfWork.Requests.GetRequestByStatus("Processing");
                                    a.AddRange(await _unitOfWork.Requests.GetRequestByStatus("Done"));
                                    return a;
                                       
                                }
                                return null;
                            }
                        case 4:
                            {
                                return await _unitOfWork.Requests.GetRequestByStatus("In-Production"); break;

                            }
                       /* case 5:
                            {
                                return _unitOfWork.Requests.GetRequestByStatus(""); break;

                            }*/
                       /* case 6:
                            {
                                return _unitOfWork.Requests.GetRequestByStatus(""); break;

                            }*/
                        default:
                            {
                                return await _unitOfWork.Requests.GetRequestByStatus(status);

                            }
                    }


                }
                else
                {
                    return null;
                }



            }
            catch (Exception ex) {

                return null;
            }

        }

        public async Task<List<StatisticRequest>?> GetRequestStatistic()
        {
           /* int targetMonth = DateTime.Now.Month;
            int targetYear = DateTime.Now.Year;*/
            int targetMonth = 12;
            int targetYear = 2012;
            List < StatisticRequest > data = new List<StatisticRequest> ();
          for (int i = 1; i<= targetMonth; i++)
            {
                var getAllinMonth =await _unitOfWork.Requests.GetRequestByTime(targetYear, i);
                if(getAllinMonth.Count>0)
                {
                    StatisticRequest statisticRequest = new StatisticRequest();

                    foreach ( var b in getAllinMonth)
                    {
                        if (b.Type == 1)
                        {
                            statisticRequest.OrderExist += 1;
                        }
                        if (b.Type == 2)
                        {
                            statisticRequest.OrderCustome += 1;
                        }
                        if (b.Type == 3)
                        {
                            statisticRequest.OrderDesign += 1;
                        }
                    }
                    statisticRequest.Time = "Month "+i;
                    data.Add(statisticRequest);
                }   
            }
            return data;
        }
    }
}
