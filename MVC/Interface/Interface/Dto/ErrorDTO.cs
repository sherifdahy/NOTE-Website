
namespace Interface.Dto
{
    public class ErrorDTO
    {
        public string Message { get; set; }
        public string Target { get; set; }
        public List<ErrorDetailsDTO> Details { get; set; }

    }
}
