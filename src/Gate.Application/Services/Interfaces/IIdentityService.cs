using Gate.Application.DTOs.Request;
using Gate.Application.DTOs.Response;
using Gate.Domain.Models;

namespace Gate.Application.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<RegisterUserResponse> RegisterIdentityUser(RegisterUserRequest userLoginRequest, int companyId);
        Task<bool> HasIdentityUser(string email);
        Task<UserLoginResponse> Login(UserLoginRequest userLoginRequest);
        Task<UserInfo> GetByEmailAsync(string email);
    }
}