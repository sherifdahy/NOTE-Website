using Entities.Models;
using Interface.Dto;

namespace Interface.Services.ApiCall
{
    public interface IApiCall
    {
        Task<ResponseDTO<T,Y>> GenerateNewTokenAsync<T,Y>(string url, applicationUser applicationUser);

        Task<ResponseDTO<T,Y>> ReceiptSubmit<T,Y>(string url, string json, string token);

        Task<ResponseDTO<T,Y>> ReceiptSubmissions<T,Y>(string url , string submissionUuid, string token);
    }
}
