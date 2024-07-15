using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;

namespace JPOS.Controller.Pages.HomePages.Checkout
{
    [IgnoreAntiforgeryToken]
    public class CheckoutModel : PageModel
    {
        private readonly ITransactionServices _transactionServices;
        private readonly IRequestService _requestService;

        public string PaypalClientId { get; set; } = "";
        public string PaypalSecret { get; set; } = "";
        public string PaypalUrl { get; set; } = "";
        public string TotalMoney { get; set; } = "";
        public string DeliveryAddress { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public Request request = new Request();
        public CheckoutModel(IConfiguration configuration, ITransactionServices transactionServices, IRequestService requestService)
        {
            PaypalClientId = configuration["PaypalSettings:ClientId"]!;
            PaypalSecret = configuration["PaypalSettings:Secret"]!;
            PaypalUrl = configuration["PaypalSettings:Url"]!;
            _transactionServices = transactionServices;
            _requestService = requestService;
            
        }
        public void OnGet()
        {
            PhoneNumber = HttpContext.Session.GetString("PhoneNum") ?? "";
            DeliveryAddress = HttpContext.Session.GetString("Address") ?? "";
            TotalMoney = TempData["TotalMoney"]?.ToString() ?? "";
            TempData.Keep();
        }

        public JsonResult OnPostCreateOrder()
        {
            string UID = TempData["UID"]?.ToString() ?? "";
            string Description = TempData["Description"]?.ToString() ?? "";
            string Status = TempData["Status"]?.ToString() ?? "";
            string PID = TempData["PID"]?.ToString() ?? "";
            string imageUpload = TempData["imageUpload"]?.ToString() ?? "";
            string Type = TempData["Type"]?.ToString() ?? "";
            TotalMoney = TempData["TotalMoney"]?.ToString() ?? "";

            TempData.Keep();

            if (TotalMoney == null)
            {
                return new JsonResult("");
            }

            JsonObject createOrderRequest = new JsonObject();
            createOrderRequest.Add("intent", "CAPTURE");

            JsonObject amount = new JsonObject();
            amount.Add("currency_code", "USD");
            amount.Add("value", TotalMoney);

            JsonObject purchaseUnit1 = new JsonObject();
            purchaseUnit1.Add("amount", amount);

            JsonArray purchaseUnits = new JsonArray();
            purchaseUnits.Add(purchaseUnit1);

            createOrderRequest.Add("purchase_units", purchaseUnits);

            string accessToken = GetPaypalAccessToken();

            string url = PaypalUrl + "/v2/checkout/orders";
            string orderId = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent(createOrderRequest.ToString(), null, "application/json");

                var responseTask = client.SendAsync(requestMessage);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var strResponse = readTask.Result;
                    var jsonResponse = JsonNode.Parse(strResponse);

                    if (jsonResponse != null)
                    {
                        orderId = jsonResponse["id"]?.ToString() ?? "";

                        //Insert into db
/*                        
                        request.UserId = UID;
                        request.Description = Description;
                        request.CreateDate = DateTime.Now;
                        request.Status = "Pending";
                        request.ProductId = Int32.Parse(PID);
                        request.Type = Int32.Parse(Type);
                        _requestService.CreateRequestAsync(request);*/
                    }
                }
            }

            var response = new
            {
                Id = orderId
            };

            return new JsonResult(response);
        }

        public async Task<JsonResult> OnPostCompleteOrder([FromBody] JsonObject data)
        {
            int requestID = Int32.Parse(TempData["RID"]?.ToString() ?? "");
            if (data == null || data["orderID"] == null)
            {
                return new JsonResult("");
            }

            var orderID = data["orderID"]!.ToString();

            string accessToken = GetPaypalAccessToken();

            string url = PaypalUrl + "/v2/checkout/orders/" + orderID + "/capture";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent("", null, "application/json");

                var responseTask = client.SendAsync(requestMessage);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var strResponse = readTask.Result;
                    var jsonResponse = JsonNode.Parse(strResponse);

                    if (jsonResponse != null)
                    {
                        string paypalOrderStatus = jsonResponse["status"]?.ToString() ?? "";
                        if(paypalOrderStatus == "COMPLETED")
                        {
                            TempData.Clear();

                            //update status
                            var oldRequest = await _requestService.GetRequestByIDAsync(requestID);
                            if(oldRequest.Type == 1 || oldRequest.Type == 2)
                            {
                                oldRequest.Status = "Completed";
                                _requestService.UpdateRequestAsync(oldRequest);
                                return new JsonResult("success");
                            }
                            else if(oldRequest.Type == 3)
                            {
                                if(oldRequest.Status == "Finished")
                                {
                                    oldRequest.Status = "Completed";
                                    _requestService.UpdateRequestAsync(oldRequest);
                                    return new JsonResult("success");
                                }
                                return new JsonResult("success");
                            }
                        }
                    }
                }
            }
            

            return new JsonResult("");
        }

        public IActionResult OnPostCancelOrder([FromBody] JsonObject data)
        {
            if (data == null || data["orderID"] == null)
            {
                return new JsonResult("");
            }

            var orderID = data["orderID"]!.ToString();

            return new JsonResult("");
        }

        private string GetPaypalAccessToken()
        {
            string accessToken = "";
            string url = PaypalUrl + "/v1/oauth2/token";

            using (var client = new HttpClient())
            {
                string credentials64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(PaypalClientId + ":" + PaypalSecret));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + credentials64);

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent("grant_type=client_credentials", null, "application/x-www-form-urlencoded");

                var responseTask = client.SendAsync(requestMessage);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var strResponse = readTask.Result;

                    var jsonResponse = JsonNode.Parse(strResponse);
                    if (jsonResponse != null)
                    {
                        accessToken = jsonResponse["access_token"]?.ToString() ?? "";
                    }
                }
            }
            return accessToken;
        }
    }
}
