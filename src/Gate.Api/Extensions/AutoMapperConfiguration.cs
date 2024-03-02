using Gate.Application.Profiles;
using AutoMapper;

namespace Gate.Api.Extensions;
public static class AutoMapperConfiguration
{
  public static void ConfigureAutoMapper(this IServiceCollection services, IConfiguration configuration) 
  { 
    var mapperConfiguration = new MapperConfiguration(cfg => {
      cfg.AddProfile<MapperProfile>();
    });

    IMapper mapper = mapperConfiguration.CreateMapper();
    services.AddSingleton(mapper);
  }
}
