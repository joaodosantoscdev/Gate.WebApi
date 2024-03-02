using Gate.Application.DTOs.Request;
using Gate.Application.DTOs.Response;
using Gate.Application.DTOs.Response.Interfaces;
using Gate.Domain.Models;

namespace Gate.Identity.BusinessLogic.Interfaces
{
    public interface IIdentityService
    {
        Task<IBaseResponse<RegisterUserResponse>> RegisterIdentityUser(RegisterUserRequest registerUserRequest);
        Task<UserLoginResponse> Login(UserLoginRequest userLoginRequest);
        Task<bool> HasIdentityUser(string email);
        Task<UserInfo> GetByEmailAsync(string email);
    }
}