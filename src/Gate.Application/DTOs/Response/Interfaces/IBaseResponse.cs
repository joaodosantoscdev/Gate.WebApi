using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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