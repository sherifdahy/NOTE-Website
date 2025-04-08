
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
        public async Task<ResponseDTO<T>> GenerateNewTokenAsync<T>(string url, ApplicationUser applicationUser)
        {
            var request = new RestRequest(url, Method.Post);

            var headers = new Dictionary<string, string>
            {
                { "posserial", applicationUser.POSSerial },
                { "pososversion", "Windows" },
                { "presharedkey", "" },
                { "Content-Type", "application/x-www-form-urlencoded" }
            };
            request.AddHeaders(headers);

            var parameters = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", applicationUser.POSClientId },
                { "client_secret", applicationUser.POSClientSecret },
                { "scope", "InvoicingAPI" }
            };
            foreach (var param in parameters)
            {
                request.AddParameter(param.Key, param.Value);
            }

            var response = await restAuth.ExecuteAsync<T>(request);
            var responseDto = new ResponseDTO<T>();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    responseDto.SuccessfulResponse = JsonConvert.DeserializeObject<T>(response.Content);
                    break;

                case HttpStatusCode.BadRequest:
                    responseDto.ErrorResponse = JsonConvert.DeserializeObject<SimpleErrorDTO>(response.Content);
                    break;

                

                default:
                    return default;
            }

            return responseDto;
        }

        public async Task<ResponseDTO<T>> ReceiptSubmissions<T>(string url, string submissionUuid, string token)
        {
            var requestUrl = $"{url}/{submissionUuid}/details?PageNo=1&PageSize=100";
            var request = new RestRequest(requestUrl, Method.Get);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Accept-Language", "ar");

            RestResponse restResponse = (RestResponse)await restClient.ExecuteAsync(request);

            var responseDto = new ResponseDTO<T>();

            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    responseDto.SuccessfulResponse = JsonConvert.DeserializeObject<T>(restResponse.Content);
                    break;

                case HttpStatusCode.Forbidden:
                    responseDto.ErrorResponse = JsonConvert.DeserializeObject<StandardErrorResponseDTO>(restResponse.Content);
                    break;

                case HttpStatusCode.UnprocessableEntity:
                    responseDto.ErrorResponse = JsonConvert.DeserializeObject<SimpleErrorDTO>(restResponse.Content);
                    break;
                default:
                    return default;
            }

            return responseDto;
        }


        public async Task<ResponseDTO<T>> ReceiptSubmit<T >(string url, string json, string token)
        {
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Accept-Language", "ar");
            request.AddJsonBody(json);

            RestResponse restResponse = (RestResponse)await restClient.ExecuteAsync(request);

            var responseDto = new ResponseDTO<T>();

            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.Accepted:
                    responseDto.SuccessfulResponse = JsonConvert.DeserializeObject<T>(restResponse.Content);
                    break;

                case HttpStatusCode.Forbidden :
                    responseDto.ErrorResponse = JsonConvert.DeserializeObject<StandardErrorResponseDTO>(restResponse.Content);
                    break;

                case HttpStatusCode.UnprocessableEntity:
                    responseDto.ErrorResponse = JsonConvert.DeserializeObject<SimpleErrorDTO>(restResponse.Content);
                    break;
                default:
                    return default;
            }

            return responseDto;
        }


    }
}
