namespace Interface.Dto
{
    public class ResponseDTO<T,Y,Z>
    {
        public T Accepted { get; set; }
        public Y BadRequest { get; set; }
        public Z UnprocessableEntity {  get; set; }
    }
}
