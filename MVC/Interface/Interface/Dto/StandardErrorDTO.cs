
namespace Interface.Dto
{
    public class StandardErrorDTO
    {
        public string Message { get; set; }
        public string Target { get; set; }
        public List<StandardErrorDetailsDTO> Details { get; set; }

    }
}
