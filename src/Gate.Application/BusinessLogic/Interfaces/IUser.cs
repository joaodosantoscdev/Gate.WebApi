using Gate.Application.DTOs.Request;
using Gate.Domain.Models;

namespace Gate.Application.BusinessLogic.Interfaces
{
    public interface IUser
    {
        UserInfo GetById(int id);
        Task<BaseInfoResponse> RegisterUser(RegisterUserRequest registerUser);
    }
}