using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Gate.Identity.Context;
using Gate.Identity.Models;
using Gate.Identity.Service;
using Gate.Persistence.Context;
using Gate.Identity.BusinessLogic.Interfaces;

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

    services.AddIdentity<ApplicationUser, ApplicationRole>()
      .AddRoleManager<RoleManager<ApplicationRole>>()
      .AddSignInManager<SignInManager<ApplicationUser>>()
      .AddRoleValidator<RoleValidator<ApplicationRole>>()
      .AddEntityFrameworkStores<IdentityDataContext>()
      .AddDefaultTokenProviders();

    services.AddScoped<IIdentityService, IdentityService>();
  }
}
