namespace Interface.Dto
{
    public class ErrorDetailsDTO
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Target { get; set; }
        public string PropertyPath { get; set; }
    }
}
