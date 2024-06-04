using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Gate.Identity.Context;
using Gate.Identity.Models;
using Gate.Identity.Service;
using Gate.Persistence.Context;
using Gate.Identity.BusinessLogic.Interfaces;
using Gate.Application.Services;
using Gate.Persistence.Repositories;
using Gate.Application.Services.Interfaces;
using Gate.Persistence.Repositories.Interfaces;

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
    services.AddScoped<IAccessService, AccessService>();
    services.AddScoped<IContactService, ContactService>();
    services.AddScoped<IResidentService, ResidentService>();
    services.AddScoped<IUnitService, UnitService>();
    services.AddScoped<IComplexService, ComplexService>();
    services.AddScoped<IPlaceService, PlaceService>();

    services.AddScoped<IAccessRepository, AccessRepository>();
    services.AddScoped<IContactRepository, ContactRepository>();
    services.AddScoped<IResidentRepository, ResidentRepository>();
    services.AddScoped<IUnitRepository, UnitRepository>();
    services.AddScoped<IComplexRepository, ComplexRepository>();
    services.AddScoped<IPlaceRepository, PlaceRepository>();
  }
}
