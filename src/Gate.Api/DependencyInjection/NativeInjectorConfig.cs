using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Gate.Identity.Context;
using Gate.Identity.Models;
using Gate.Identity.Service;
using Gate.Persistence.Context;
using Gate.Identity.BusinessLogic.Interfaces;
using Gate.Application.Services;
using Gate.Persistence.Repositories.Interfaces;
using Gate.Persistence.Repositories;

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

    services.AddIdentity<ApplicationUser, IdentityRole<int>>()
      .AddEntityFrameworkStores<IdentityDataContext>()
      .AddDefaultTokenProviders()
      .AddRoles<IdentityRole<int>>();

    services.AddScoped<IIdentityService, IdentityService>();
    services.AddScoped<IComplexService, ComplexService>();

    services.AddScoped<IComplexRepository, ComplexRepository>();
  }
}
