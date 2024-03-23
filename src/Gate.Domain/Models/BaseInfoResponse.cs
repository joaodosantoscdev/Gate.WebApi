namespace Gate.Domain.Models
{
    public struct EntityResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public EntityResponse() => Success = false;

        public EntityResponse ReturnSuccess() { 
            return new EntityResponse { Success = true };
        }

        public EntityResponse ReturnError(string message) { 
            return new EntityResponse { Message = message };
        } 
    }
}