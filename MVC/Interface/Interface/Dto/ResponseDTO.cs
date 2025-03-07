namespace Interface.Dto
{
    public class ResponseDTO<T,Y>
    {
        public T SuccessResponse { get; set; }
        public Y ErrorResponse { get; set; }
    }
}
