
using Entities.ConfigurationSettings;
using Entities.Models;
using Newtonsoft.Json;
using NuGet.Common;
using RestSharp;

namespace Interface.Services.ApiCall
{
    public class ApiCall : IApiCall
    {
        private readonly string baseUrl;
        private readonly string idSrvBaseUrl;
        private readonly RestClient restClient;
        private readonly RestClient restAuth;
        public ApiCall(IConfiguration configuration)
        {
            idSrvBaseUrl = configuration.GetSection("api")["idSrvBaseUrl"];
            baseUrl = configuration.GetSection("api")["baseUrl"];
            restClient = new RestClient(baseUrl);
            restAuth = new RestClient(idSrvBaseUrl);
        }
        public async Task<T> GenerateNewTokenAsync<T>(string url, applicationUser applicationUser)
        {
            var request = new RestRequest(url, Method.POST);
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
            return response.IsSuccessful ? JsonConvert.DeserializeObject<T>(response.Content) : default;


        }

        public async Task<T> Submissions<T>(string url, string json, string token)
        {
            var request = new RestRequest(url, Method.POST);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddJsonBody(json);
            RestResponse response = (RestResponse)await restClient.ExecuteAsync(request);
            return response.IsSuccessful ? JsonConvert.DeserializeObject<T>(response.Content) : default;
        }

    }
}
