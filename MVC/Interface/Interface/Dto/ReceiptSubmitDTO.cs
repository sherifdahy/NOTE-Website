using ETA.eReceipt.IntegrationToolkit.Application.Dtos;
using ETA.eReceipt.IntegrationToolkit.Application.Models;
using Newtonsoft.Json;

namespace Interface.Dto
{
    public class ReceiptSubmitDTO : BaseResponseDto
    {
        public string? submissionId { get; set; }

        public List<ReceiptsAcceptedDto>? acceptedDocuments { get; set; }

        public List<ReceiptsRejectedDto>? rejectedDocuments { get; set; }

        public HeaderDto? header { get; set; }
    }

    public class ReceiptsAcceptedDto
    {
        public string submissionUuid { get; set; } = null;


        public string uuid { get; set; } = null;


        public string longId { get; set; } = null;


        public string receiptNumber { get; set; } = null;


        public string hashKey { get; set; } = null;
    }

    public class ReceiptsRejectedDto
    {
        public string receiptNumber { get; set; } = null;


        public string uuid { get; set; } = null;


        public HttpCustomErrorResponseModel error { get; set; } = null;
    }

    public class HeaderDto
    {


        public string? statusCode { get; set; }
        public string? code { get; set; }
        public List<string>? details { get; set; }
        public string? correlationId { get; set; }
        public string? requestTime { get; set; }
        public long responseProcessingTimeInTicks { get; set; }
    }

}
