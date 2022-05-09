using System;

namespace Core.Shared.Models.Responses
{
    public class ErrorResponse
    {
        public string Id { get; set; }

        public DateTime Data { get; set; }

        public string Message { get; set; }
    
        public ErrorResponse(string errorId)
        {
            Id = errorId;
            Data = DateTime.Now;
            Message = "Erro Inesperado.";
        }
    }
}
