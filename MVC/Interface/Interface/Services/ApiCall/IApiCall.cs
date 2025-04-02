using Entities.Models;
using Interface.Dto;

namespace Interface.Services.ApiCall
{
    public interface IApiCall
    {
        Task<ResponseDTO<T,Y,Z>> GenerateNewTokenAsync<T,Y, Z>(string url, ApplicationUser applicationUser);

        Task<ResponseDTO<T,Y,Z>> ReceiptSubmit<T,Y, Z>(string url, string json, string token);

        Task<ResponseDTO<T,Y,Z>> ReceiptSubmissions<T,Y, Z>(string url , string submissionUuid, string token);
    }
}
