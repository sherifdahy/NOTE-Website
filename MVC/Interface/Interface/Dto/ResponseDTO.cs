namespace Interface.Dto
{
    public class ResponseDTO<T>
    {
        public T SuccessfulResponse { get; set; } 
        public dynamic ErrorResponse { get; set; } 
    }
}
