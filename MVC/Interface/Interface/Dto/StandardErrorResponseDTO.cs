namespace Interface.Dto
{
    public class StandardErrorResponseDTO
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Target { get; set; }
        public List<StandardErrorDetailsDTO> Details { get; set; } = new List<StandardErrorDetailsDTO>();
    }
    


}
