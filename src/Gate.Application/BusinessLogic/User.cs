using Gate.Application.BusinessLogic.Interfaces;
using Gate.Persistence.DataAccess.Interfaces;
using Gate.Application.Facade.API;
using Gate.Domain.Models;
using Gate.Application.DTOs.Request;
using Gate.Application.Services.Interfaces;
using MySqlConnector;

namespace Gate.Application.BusinessLogic
{
  public class User : IUser
  {
    private MySqlConnection _connection;
    private IIdentityService _identityService;
    private FacadeUser _facadeUser;
    private readonly IUserDal _userDal;
    private readonly ICompany _company;
    private readonly IEmployee _employee;
    private readonly IUnity _unity;
    public User(IIdentityService identityService, 
                ICompany company, IEmployee employee, 
                IUnity unity, IUserDal userDal)
    {
      _company = company;
      _employee = employee;
      _unity = unity;
      _userDal = userDal;
      _identityService = identityService;
      _facadeUser = new FacadeUser();
    }

    public UserInfo GetById(int id) => _userDal.GetById(id);

    private MySqlTransaction BeginTransaction()
    {
      _connection = _userDal.OpenConnection();

      return _connection.BeginTransaction();
    }

    private void CloseTransaction(MySqlTransaction trans)
    {
      trans.Commit();
      trans.Connection?.Close();
    }

    public async Task<BaseInfoResponse> RegisterUser(RegisterUserRequest userRequest)
    {
      var trans = BeginTransaction();
      var baseInfoResponse = new BaseInfoResponse();

      try
      {
        if (_facadeUser.CreateServiceUser(userRequest.Description, userRequest.Password))
        {
          var company = new CompanyInfo { Description = userRequest.userCompany.Description };
          var companyInfo = _company.Post(company, trans);

          if (await _identityService.HasIdentityUser(userRequest.Email))
            return baseInfoResponse.ReturnError($"Usuário com E-mail {userRequest.Email} já registrado.");

          var registerUserResponse = await _identityService.RegisterIdentityUser(userRequest, companyInfo.Id);

          foreach (var userUnity in userRequest.userUnityList)
          {
            var unityInfo = _unity.GetByTaxId(userUnity.TaxId);
            if (unityInfo.Id > 0)
              return baseInfoResponse.ReturnError($"Unidade com CNPJ {userUnity.TaxId} já registrada");

            unityInfo = new UnityInfo
            {
              TaxId = userUnity.TaxId,
              Active = true,
              CompanyId = companyInfo.Id,
              CreationDate = DateTime.Now
            };

            unityInfo = _unity.Post(unityInfo);

            var employeeInfo = _employee.GetByRegister(userRequest.Register);
            if (employeeInfo.Id > 0)
              return baseInfoResponse.ReturnError($"Colaborador já registrado para o CPF {userRequest.Cpf}");

            employeeInfo = new EmployeeInfo
            {
              UnityId = unityInfo.Id,
              UserId = registerUserResponse.UserInfo.Id,
              Register = userRequest.Register,
              Cpf = userRequest.Cpf,
              Birthdate = userRequest.Birthdate,
              Phone = userRequest.Phone,
            };

            _employee.Post(employeeInfo, trans);
          }
        }
        else {
          return baseInfoResponse.ReturnError("Falha ao criar usuário na API Octoprint");
        }
      }
      catch (Exception ex)
      {
        throw new ArgumentException($"Falha ao criar estrutura do usuário. StackTrace: ${ex.StackTrace}", ex.Message);
      }
      finally
      {
        CloseTransaction(trans);
      }
      
      return baseInfoResponse;
    }
  }
}