﻿namespace Interface.Dto
{
    public class ErrorResponseDTO
    {
        public ErrorDetails Error { get; set; }
    }

    public class ErrorDetails
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Target { get; set; }
    }
}
