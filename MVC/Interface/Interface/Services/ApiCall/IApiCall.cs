using Entities.Models;
using Interface.Dto;

namespace Interface.Services.ApiCall
{
    public interface IApiCall
    {
        Task<ResponseDTO<T>> GenerateNewTokenAsync<T>(string url, ApplicationUser applicationUser);

        Task<ResponseDTO<T>> ReceiptSubmit<T>(string url, string json, string token);

        Task<ResponseDTO<T>> ReceiptSubmissions<T>(string url , string submissionUuid, string token);
    }
}
