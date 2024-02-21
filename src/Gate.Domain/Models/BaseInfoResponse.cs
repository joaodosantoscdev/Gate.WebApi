namespace Gate.Domain.Models
{
    public struct BaseInfoResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public BaseInfoResponse() => Success = false;

        public BaseInfoResponse ReturnSuccess() { 
            return new BaseInfoResponse { Success = true };
        }

        public BaseInfoResponse ReturnError(string message) { 
            return new BaseInfoResponse { Message = message };
        } 
    }
}