using Gate.Application.BusinessLogic.Interfaces;
using Gate.Persistence.DataAccess.Interfaces;
using Gate.Domain.Models;
using Gate.Application.Services.Interfaces;
using Gate.Application.DTOs.Request;
using Gate.Application.DTOs.Response;
using Gate.Application.DTOs.Response.Interfaces;

namespace Gate.Application.Services
{
    public class UserService : IUserService
    {
    private readonly IUserDal _userDal;
    private readonly IUser _user;
    public UserService(IUser user, IUserDal userDal)
    {
        _user = user;
        _userDal = userDal;
    }

    public List<UserInfo> GetAll()
    {
      try
      {
        var userList = _userDal.GetAll();
        
        if (!userList.Any())
            throw new NullReferenceException("Não há usuários cadastrados.");

        return userList;
      }
      catch (Exception ex)
      {
         throw new Exception($"Falha ao recuperar os usuários: {ex.Message}");
      }

    }

    public UserInfo GetById(int id)
    {
        try
        {
            var user = _userDal.GetById(id);
            if(user == null) 
                throw new NullReferenceException("Usuário não encontrado.");

            return user;
        }
        catch (Exception ex)
        {
            throw new Exception($"Falha ao recuperar dados do usuário: {ex.Message}");
        }
    }

    public async Task<IBaseResponse> RegisterUser(RegisterUserRequest registerUser)
    {
        var baseInfoResponse = await _user.RegisterUser(registerUser);

        return baseInfoResponse.Success ? BaseResponse.CreateSuccessResponse()
            : BaseResponse.CreateErrorResponse(baseInfoResponse.Message);
    }
  }
}