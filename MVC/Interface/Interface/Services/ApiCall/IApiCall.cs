using Entities.Models;

namespace Interface.Services.ApiCall
{
    public interface IApiCall
    {
        Task<T> GenerateNewTokenAsync<T>(string url, applicationUser applicationUser);

        Task<T> Submissions<T>(string url, string json, string token);
    }
}
