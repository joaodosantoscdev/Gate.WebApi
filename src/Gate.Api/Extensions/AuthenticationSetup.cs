using System.Text;
using Gate.Identity.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Gate.Api.Extensions;
public static class AuthenticationSetup
{
  public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
  {
      var jwtAppSettingOptions = configuration.GetSection(nameof(JwtOptions));
      var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JwtOptions:SecurityKey").Value));

      services.Configure<JwtOptions>(opt =>
      {
          opt.Issuer = jwtAppSettingOptions[nameof(JwtOptions.Issuer)];
          opt.Audience = jwtAppSettingOptions[nameof(JwtOptions.Audience)];
          opt.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
          opt.RefreshTokenExpiration = int.Parse(jwtAppSettingOptions[nameof(JwtOptions.RefreshTokenExpiration)] ?? "0");
          opt.AccessTokenExpiration = int.Parse(jwtAppSettingOptions[nameof(JwtOptions.AccessTokenExpiration)] ?? "0");
      });

      services.Configure<IdentityOptions>(opt =>
      {
            opt.Password.RequireDigit = false;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequiredLength = 4;
      });

      var tokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidIssuer = configuration.GetSection("JwtOptions:Issuer").Value,

          ValidateAudience = true,
          ValidAudience = configuration.GetSection("JwtOptions:Audience").Value,

          ValidateIssuerSigningKey = true,
          IssuerSigningKey = securityKey,

          RequireExpirationTime = true,
          ValidateLifetime = true,

          ClockSkew = TimeSpan.Zero
      };

      services.AddAuthentication(opt =>
      {
          opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(opt => 
      {
          opt.TokenValidationParameters = tokenValidationParameters;
      });
  }

//   public static void AddAuthorizationPolicies(this IServiceCollection services)
//   {
//       // services.AddSingleton<IAuthorizationHandler, HorarioComercialHandler>();
//       // services.AddAuthorization(opt =>
//       // {
//       //     opt.AddPolicy(Policies.HorarioComercial, policy =>
//       //         policy.Requirements.Add(new HorarioComercialRequirement()));
//       // });
//   }
}
