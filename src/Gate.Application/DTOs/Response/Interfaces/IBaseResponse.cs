namespace Gate.Application.DTOs.Response.Interfaces
{
    public interface IBaseResponse
    {
        string Message { get; set; }
        bool Success { get; set; }
    }

    
    public interface IBaseResponse<T> : IBaseResponse
    {
        T Data { get; set; }
    }
}