namespace Interface.Dto
{
    


    public class ReceiptSubmissionsDTO
    {
        public string SubmissionUuid { get; set; }
        public string SellerId { get; set; }
        public DateTime DateTimeReceived { get; set; }
        public string SubmitterName { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string SubmissionChannel { get; set; }
        public int ReceiptsCount { get; set; }
        public int InvalidReceiptsCount { get; set; }
        public string Status { get; set; }
        public List<ReceiptDTO> Receipts { get; set; }
        public MetadataDTO Metadata { get; set; }
    }

    public class ReceiptDTO
    {
        public string Uuid { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime DateTimeIssued { get; set; }
        public DateTime DateTimeReceived { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTypeNamePrimaryLang { get; set; }
        public string DocumentTypeNameSecondaryLang { get; set; }
        public string Currency { get; set; }
        public string CurrencyNamePrimaryLang { get; set; }
        public string CurrencyNameSecondaryLang { get; set; }
        public string DocumentTypeVersion { get; set; }
        public double TotalAmount { get; set; }
        public double ExchangeRate { get; set; }
        public string SellerID { get; set; }
        public string SellerName { get; set; }
        public string BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerType { get; set; }
        public string Status { get; set; }
        public string LongId { get; set; }
        public string DeviceSerialNumber { get; set; }
        public bool HasReturnReceipts { get; set; }
        public List<ErrorDTO> Errors { get; set; }
    }

    public class ErrorDTO
    {
        public string StepId { get; set; }
        public string StepName { get; set; }
        public string StepNameAr { get; set; }
        public ErrorDetailsDTO Error { get; set; }
    }

    public class ErrorDetailsDTO
    {
        public string ErrorCode { get; set; }
        public string Error { get; set; }
        public string ErrorAr { get; set; }
        public List<InnerErrorDTO> InnerError { get; set; }
    }

    public class InnerErrorDTO
    {
        public string PropertyPath { get; set; }
        public string ErrorCode { get; set; }
        public string Error { get; set; }
        public string ErrorAr { get; set; }
    }

    public class MetadataDTO
    {
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPageNo { get; set; }
    }




}
