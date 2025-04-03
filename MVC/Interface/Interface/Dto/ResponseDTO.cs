namespace Interface.Dto
{
    public class ResponseDTO<T>
    {
        public T SuccessfulResponse { get; set; } 
        public object ErrorResponse { get; set; } 
    }
}
