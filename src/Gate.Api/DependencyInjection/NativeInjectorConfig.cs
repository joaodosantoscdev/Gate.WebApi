using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Gate.Application.BusinessLogic;
using Gate.Application.BusinessLogic.Interfaces;
using Gate.Application.Services.Interfaces;
using Gate.Application.Services;
using Gate.Identity.Context;
using Gate.Identity.Models;
using Gate.Identity.Service;
using Gate.Persistence.Context;
using Gate.Persistence.DataAccess;
using Gate.Persistence.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Gate.Api.DependencyInjection;
public static class NativeInjectorConfig
{
  public static void RegisterServices(this IServiceCollection services, IConfiguration configuration) 
  {
    string mySqlConnection = configuration.GetConnectionString("gate");
    services.AddDbContextPool<AppDbContext>(
      opt => opt.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection))
    );

    services.AddDbContext<IdentityDataContext>(
      opt => opt.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection))
    );

    services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDataContext>()
    .AddDefaultTokenProviders();

    services.AddScoped<IIdentityService, IdentityService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IUser, User>();
    services.AddScoped<IEmployee, Employee>();
    services.AddScoped<IUnity, Unity>();
    services.AddScoped<ICompany, Company>();

    services.AddScoped<IBaseDal, BaseDal>();
    services.AddScoped<IUserDal, UserDal>();
    services.AddScoped<IEmployeeDal, EmployeeDal>();
    services.AddScoped<IUnityDal, UnityDal>();
    services.AddScoped<ICompanyDal, CompanyDal>();
    services.AddScoped<ISecurityDal, SecurityDal>();
  }
}
