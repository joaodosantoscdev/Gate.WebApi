using Gate.Application.DTOs.Response.Interfaces;


namespace Gate.Application.DTOs.Response
{
    public class BaseResponse : IBaseResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public static BaseResponse<T> CreateErrorResponse<T>(T data, string errorMessage)
        {
            BaseResponse<T> baseResponse = new BaseResponse<T>
            {
                Success = false,
                Data = data,
                Message = errorMessage
            };

            return baseResponse;
        }

        public static BaseResponse<T> CreateErrorResponse<T>(T data) {
            return CreateErrorResponse(data, string.Empty);
        }

        public static BaseResponse<T> CreateErrorResponse<T>(string errorMessage) where T : class, new() {
            return CreateErrorResponse(new T(), errorMessage);
        }

        public static BaseResponse<T> CreateErrorResponse<T>() where T : class, new() {
            return CreateErrorResponse(new T(), string.Empty);
        }

        public static BaseResponse<T> CreateSuccessResponse<T>(T data, string message)
        {
            BaseResponse<T> baseResponse = new BaseResponse<T>
            {
                Success = true,
                Data = data,
                Message = message
            };

            return baseResponse;
        }

        public static BaseResponse<T> CreateSuccessResponse<T>(T data) {
            return CreateSuccessResponse(data, string.Empty);
        }

        public static BaseResponse<T> CreateSuccessResponse<T>(string message) where T : class, new() {
            return CreateSuccessResponse(new T(), message);
        }

        public static BaseResponse CreateSuccessResponse(string message)
        {
            BaseResponse baseResponse = new BaseResponse
            {
                Success = true,
                Message = message
            };

            return baseResponse;
        }

        public static BaseResponse CreateSuccessResponse() {
            return CreateSuccessResponse(string.Empty);
        }
    }

    public class BaseResponse<T> : IBaseResponse, IBaseResponse<T>
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}