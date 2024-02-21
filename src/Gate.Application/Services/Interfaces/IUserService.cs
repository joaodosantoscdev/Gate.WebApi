using Gate.Application.DTOs.Request;
using Gate.Application.DTOs.Response.Interfaces;
using Gate.Domain.Models;

namespace Gate.Application.Services.Interfaces
{
    public interface IUserService
    {
        List<UserInfo> GetAll();
        UserInfo GetById(int id);
        Task<IBaseResponse> RegisterUser(RegisterUserRequest registerUser);
    }
}