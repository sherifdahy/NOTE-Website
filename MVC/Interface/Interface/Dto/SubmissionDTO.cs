using Newtonsoft.Json;

namespace Interface.Dto
{
    public class SubmissionDTO
    {
        [JsonProperty("submissionId")]
        public string SubmissionId { get; set; }

        [JsonProperty("acceptedDocuments")]
        public List<AcceptedDocument> AcceptedDocuments { get; set; }

        [JsonProperty("rejectedDocuments")]
        public List<object> RejectedDocuments { get; set; } // فارغ في المثال، يمكن تغييره لاحقًا

        [JsonProperty("header")]
        public Header Header { get; set; }
    }

    public class AcceptedDocument
    {
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("longId")]
        public string LongId { get; set; }

        [JsonProperty("receiptNumber")]
        public string ReceiptNumber { get; set; }

        [JsonProperty("hashKey")]
        public string HashKey { get; set; }
    }

    public class Header
    {
        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("details")]
        public List<object> Details { get; set; }

        [JsonProperty("correlationId")]
        public string CorrelationId { get; set; }

        [JsonProperty("requestTime")]
        public DateTime RequestTime { get; set; }

        [JsonProperty("responseProcessingTimeInTicks")]
        public long ResponseProcessingTimeInTicks { get; set; }
    }

}
