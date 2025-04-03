namespace Interface.Dto.ReceiptSubmit
{
    public class ErrorResponseDTO
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Target { get; set; }
        public List<ErrorDetailsDTO> Details { get; set; } = new List<ErrorDetailsDTO>();
    }
    


}
