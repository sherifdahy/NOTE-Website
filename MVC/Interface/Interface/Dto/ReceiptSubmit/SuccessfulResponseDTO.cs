
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
        public string uuid { get; set; } = null;
        public string longId { get; set; } = null;
        public string receiptNumber { get; set; } = null;
    }

    public class DocumentRejectedDTO
    {
        public string receiptNumber { get; set; } = null;
        public string uuid { get; set; } = null;
        public ErrorDTO error { get; set; } = null;
    }

    



}
