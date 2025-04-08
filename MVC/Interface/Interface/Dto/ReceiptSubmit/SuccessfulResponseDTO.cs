
namespace Interface.Dto.ReceiptSubmit
{
    public class SuccessfulResponseDTO
    {
        public string? submissionId { get; set; }
        public List<DocumentAcceptedDTO>? acceptedDocuments { get; set; }
        public List<DocumentRejectedDTO>? rejectedDocuments { get; set; }
    }

    public class DocumentAcceptedDTO
    {
        public string uuid { get; set; } 
        public string longId { get; set; } 
        public string receiptNumber { get; set; } 
    }

    public class DocumentRejectedDTO
    {
        public string receiptNumber { get; set; } 
        public string uuid { get; set; } 
        public StandardErrorDTO error { get; set; } 
    }

    



}
