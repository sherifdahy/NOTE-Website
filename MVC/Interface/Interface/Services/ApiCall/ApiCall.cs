
using Entities.ConfigurationSettings;
using Entities.Models;
using Interface.ApplicationConfiguration;
using Interface.Dto;
using Newtonsoft.Json;
using NuGet.Common;
using RestSharp;
using System.Net;

namespace Interface.Services.ApiCall
{
    public class ApiCall : IApiCall
    {
        private readonly RestClient restClient;
        private readonly RestClient restAuth;
        public ApiCall(IAppConfig appConfig)
        {
            restClient = new RestClient(appConfig.baseUrl);
            restAuth = new RestClient(appConfig.idSrvBaseUrl);
        }
        public async Task<ResponseDTO<T,Y>> GenerateNewTokenAsync<T,Y>(string url, applicationUser applicationUser)
        {
            var request = new RestRequest(url, Method.Post);
            var headers = new Dictionary<string, string>
            {
                { "posserial", applicationUser.POSSerial },
                { "pososversion", "Windows" },
                { "presharedkey", "" },
                { "Content-Type", "application/x-www-form-urlencoded" }
            };
            var parameters = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", applicationUser.POSClientId },
                { "client_secret", applicationUser.POSClientSecret },
                { "scope", "InvoicingAPI" }
            };
            request.AddHeaders(headers);
            foreach (var param in parameters)
            {
                request.AddParameter(param.Key, param.Value);
            }
            var response = await restAuth.ExecuteAsync<T>(request);
            return response.StatusCode == HttpStatusCode.OK ? new ResponseDTO<T, Y>() { SuccessResponse = JsonConvert.DeserializeObject<T>(response.Content) } :
                response.StatusCode == HttpStatusCode.BadRequest ? new ResponseDTO<T, Y>() { ErrorResponse = JsonConvert.DeserializeObject<Y>(response.Content) } : 
                default;
        }

        public async Task<ResponseDTO<T, Y>> ReceiptSubmissions<T, Y>(string url, string submissionUuid, string token)
        {
            
            var request = new RestRequest(url + "/" + submissionUuid + "/details?PageNo=1&PageSize=100", Method.Get);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Accept-Language", "ar");

            RestResponse restResponse = (RestResponse)await restClient.ExecuteAsync(request);
            
            return 
                restResponse.StatusCode == HttpStatusCode.OK ? new ResponseDTO<T, Y>() { SuccessResponse = JsonConvert.DeserializeObject<T>(restResponse.Content) } :
                restResponse.StatusCode == HttpStatusCode.Forbidden ? new ResponseDTO<T,Y>() { ErrorResponse = JsonConvert.DeserializeObject<Y>(restResponse.Content) } : 
                default;
        }


        public async Task<ResponseDTO<T, Y>> ReceiptSubmit<T, Y>(string url, string json, string token)
        {
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Accept-Language", "ar");

            request.AddJsonBody(json);
            RestResponse restResponse = (RestResponse)await restClient.ExecuteAsync(request);
            
            return 
                restResponse.StatusCode == HttpStatusCode.Accepted ? new ResponseDTO<T, Y>() { SuccessResponse = JsonConvert.DeserializeObject<T>(restResponse.Content) } :
                restResponse.StatusCode == HttpStatusCode.Forbidden ? new ResponseDTO<T, Y>() { ErrorResponse = JsonConvert.DeserializeObject<Y>(restResponse.Content)} :
                default;
        }

       
    }
}
